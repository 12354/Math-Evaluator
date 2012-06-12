using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathEvaluator.MathOperations;
using MathEvaluator.MathOperations.Operators;
using MathEvaluator.MathOperations.Functions;
namespace MathEvaluator
{
    public class MathExpression
    {
        public StaticStack<MathOperation> ExpressionStack { get { return new StaticStack<MathOperation>(m_ExpressionStack);/*Readonly*/ } }
        public string OriginalExpression { get; private set; }
        StaticStack<MathOperation> m_ExpressionStack;
        Dictionary<string, float> m_Variables;
        ExpressionTree m_ExpressionTree;
        public ExpressionTree ExpressionTree { get { return m_ExpressionTree; } }
        public bool IsImplicit { get; private set; }
        public string PostFixExpression
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (MathOperation o in m_ExpressionStack)
                {
                    builder.Append(o + " ");
                }
                return builder.ToString();
            }
        }
        public MathExpression(StaticStack<MathOperation> _ExpressionStack,Dictionary<string,float> defaultVariables, string originalExpression)
        {
            m_ExpressionStack = _ExpressionStack;
            OriginalExpression = originalExpression;
            m_Variables = defaultVariables;
            if (m_ExpressionStack.Count == 0)
                m_ExpressionStack.Push(new Number(0));
            if (m_ExpressionStack.Peek() is Operator)
            {
                IsImplicit = ((Operator)(m_ExpressionStack.Peek())).IsLogical;
            }
            UpdateExpressionTree();
        }
        private void UpdateExpressionTree()
        {
            m_ExpressionTree = new ExpressionTree(m_ExpressionStack.Pop());
            m_ExpressionTree.BuildBinaryTree(m_ExpressionStack);
            m_ExpressionStack.ResetTop();
        }
        public MathExpression Derive(MathState state)
        {
            StaticStack<MathOperation> stack = m_ExpressionStack;
            StaticStack<MathOperation> derived = new StaticStack<MathOperation>();
            ExpressionTree tree = new ExpressionTree(stack);
            tree.DeriveToStack(derived, state);
            
            Dictionary<string, float> var = new Dictionary<string, float>(m_Variables);

            for (int i = 0; i < derived.Count; i++)
            {
                if (derived[i] is Variable)
                {
                    Variable v = (Variable)derived[i];
                    derived[i] = new Variable(v.ToString(), var);
                }

            }
            return new MathExpression(derived,var, "(" + OriginalExpression + ")'");
        }
        public MathExpression Optimize(MathState state)
        {
            ExpressionTree tree = m_ExpressionTree;
            m_ExpressionTree.Optimize(state);
            StaticStack<MathOperation> stack = m_ExpressionTree.BuildExpressionStack(new StaticStack<MathOperation>());

            Dictionary<string, float> var = new Dictionary<string, float>(m_Variables);
            for (int i = 0; i < stack.Count; i++)
            {
                if (stack[i] is Variable)
                {
                    Variable v = (Variable)stack[i];
                    stack[i] = new Variable(v.ToString(), var);
                }

            }

            MathExpression optimized = new MathExpression(stack, var, OriginalExpression);
            UpdateExpressionTree();
            return optimized;
        }
        public float Calculate()
        {
            m_ExpressionStack.ResetTop();
            if (m_ExpressionStack.Count == 0)
                return 0;
            float result = m_ExpressionStack.Pop().Calculate(m_ExpressionStack);
            m_ExpressionStack.ResetTop();
            return result;

        }
        public bool ContainsFunction(string function)
        {
            function = function.ToLower();
            foreach (MathOperation o in m_ExpressionStack)
            {
                if (o is Function)
                {
                    Function var = (Function)o;
                    if (var.ToString().ToLower() == function)
                        return true;
                }
            }
            return false;
        }
        public bool ContainsVariable(string variable)
        {
            variable = variable.ToLower();
            foreach (MathOperation o in m_ExpressionStack)
            {
                if (o is Variable)
                {
                    Variable var = (Variable)o;
                    if (var.ToString().ToLower() == variable)
                        return true;
                }
            }
            return false;
        }
        public void SetVariable(string variable, float value)
        {
            m_Variables[variable] = value;
        }
    }
}
