using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathEvaluator.MathOperations;
using MathEvaluator.MathOperations.Operators;
namespace MathEvaluator
{
    public class ExpressionTree
    {
        public ExpressionTree Left;
        public ExpressionTree Right;
        public List<ExpressionTree> FunctionParameters;
        public MathOperations.MathOperation Value;
        public ExpressionTree(MathOperations.MathOperation val)
        {
            Value = val;
        }
        public ExpressionTree(MathOperation l, MathOperation val, MathOperation r)
        {
            Value = val;
            Left = new ExpressionTree(l);
            Right = new ExpressionTree(r);
        }
        public ExpressionTree(StaticStack<MathOperation> NumStaticStack)
        {
            Value = NumStaticStack.Pop();
            BuildBinaryTree(NumStaticStack);
        }
        public override string ToString()
        {
            return Value.ToString();
        }
        public void PrintInFix(bool First = true)
        {

            if (Left != null)
            {
                if(!First)
                    Console.Write("(");
                Left.PrintInFix(false);
            }
            Console.Write(" " + Value + " ");
            if (Right != null)
            {
                Right.PrintInFix(false);
                if (!First)
                Console.Write(")");
            }
            if (FunctionParameters != null && FunctionParameters.Count > 0)
            {
                Console.Write("(");
                foreach (var v in FunctionParameters)
                    v.PrintInFix();
                Console.Write(")");
            }
            
        }
        public void PrintPreFix(int Depth = 0)
        {
            if (Left != null)
                Left.PrintPreFix(Depth + 1);
            for (int i = 0; i < Depth; i++)
                Console.Write("   ");
            Console.WriteLine(Value);
            if (Right != null)
                Right.PrintPreFix(Depth + 1);
            if (FunctionParameters != null)
                foreach (var v in FunctionParameters)
                    v.PrintPreFix(Depth + 1);
        }
        public void CopyToStack(StaticStack<MathOperations.MathOperation> Stack)
        {
            Value.CopyToStack(this, Stack);
        }
        public void DeriveToStack(StaticStack<MathOperations.MathOperation> Stack,MathState State)
        {
            Value.DeriveToStack(this,State,Stack);
        }
        public StaticStack<MathOperation> BuildExpressionStack(StaticStack<MathOperations.MathOperation> Stack)
        {
            if (FunctionParameters != null)
                foreach (var v in FunctionParameters)
                    v.BuildExpressionStack(Stack);
            else
            {
                if(Left != null)
                    Left.BuildExpressionStack(Stack);
                if (Right != null)
                Right.BuildExpressionStack(Stack);
            }
            Stack.Push(Value);
            return Stack;
        }
        public StaticStack<MathOperation> GetExpressionStack(StaticStack<MathOperations.MathOperation> Stack)
        {
            if (FunctionParameters != null)
                foreach (var v in FunctionParameters)
                    v.GetExpressionStack(Stack);
            else
            {
                Stack.Push(Left.Value);
                Stack.Push(Right.Value);
            }
            Stack.Push(Value);
            return Stack;
        }
        public void ReplaceWith(ExpressionTree tree)
        {
            Value = tree.Value;
            Right = tree.Right;
            Left = tree.Left;

            FunctionParameters = tree.FunctionParameters;
        }
        
           
        public void ReplaceWith(Number n)
        {
            Value = n;
            Right = null;
            Left = null;

            FunctionParameters = null;
        }
        public void ReplaceWith(MathOperation l, MathOperation val, MathOperation r)
        {
            Value = val ;
            Left = new ExpressionTree(l);
            Right = new ExpressionTree(r);
        }
        public bool ContainsVariable(string name)
        {
            name = name.ToLower();
            if (Value is Variable && Value.ToString() == name)
                return true;
            if (FunctionParameters != null)
                foreach (var v in FunctionParameters)
                    if (v.ContainsVariable(name))
                        return true;
            if (Left != null && Left.ContainsVariable(name))
                return true;
            if (Right != null && Right.ContainsVariable(name))
                return true;
            return false;
        }
        public void Optimize(MathState State)
        {
            if(Value is Operator)
            {
                if (Left != null)
                    Left.Optimize(State);
                if (Right != null)
                    Right.Optimize(State);

                float LeftValue = (Left.Value is Number) ? ((Number)Left.Value).Value : float.NaN;
                float RightValue = (Right.Value is Number) ? ((Number)Right.Value).Value : float.NaN;
                switch (Value.ToString())
                {
                    case "-":
                    case "+":
                        if (LeftValue == 0)
                            ReplaceWith(Right);
                        else if (RightValue == 0)
                            ReplaceWith(Left);
                        else if (!(float.IsNaN(LeftValue) || float.IsNaN(RightValue)))
                        {
                            Number result;
                            if (Value.ToString() == "+")
                                result = LeftValue + RightValue;
                            else
                                result = LeftValue - RightValue;
                            ReplaceWith(result);
                        }
                        else if (Left.Value is Plus || Left.Value is Minus)
                        {
                            // (a + b) + c
                            float LeftLeftValue = (Left.Left.Value is Number) ? ((Number)Left.Left.Value).Value : float.NaN;
                            float LeftRightValue = (Left.Right.Value is Number) ? ((Number)Left.Right.Value).Value : float.NaN;
                            float mod = Left.Value is Plus ? 1 : -1;
                            if (!(float.IsNaN(RightValue) || float.IsNaN(LeftLeftValue)))
                                Left.Left.Value = (Number)(LeftLeftValue + RightValue * mod);
                            else if (!(float.IsNaN(RightValue) || float.IsNaN(LeftRightValue)))
                                Left.Right.Value = (Number)(LeftRightValue + RightValue * mod);
                            else break;
                            ReplaceWith(Left);
                        }
                        else if (Right.Value is Plus || Right.Value is Minus)
                        {
                            // a + (b + c)
                            float RightLeftValue = (Right.Left.Value is Number) ? ((Number)Right.Left.Value).Value : float.NaN;
                            float RightRightValue = (Right.Right.Value is Number) ? ((Number)Right.Right.Value).Value : float.NaN;
                            float mod = Right.Value is Plus ? 1 : -1;
                            if (!(float.IsNaN(LeftValue) || float.IsNaN(RightLeftValue)))
                                Right.Left.Value = (Number)(RightLeftValue + LeftValue * mod);
                            else if (!(float.IsNaN(LeftValue) || float.IsNaN(RightRightValue)))
                                Right.Right.Value = (Number)(RightRightValue + LeftValue * mod);
                            else break;
                            ReplaceWith(Right);
                        }
                        break;
                    case "*":
                        if (LeftValue == 0 || RightValue == 0)
                        {
                            ReplaceWith(0);
                        }
                        else if (LeftValue == 1)
                            ReplaceWith(Right);
                        else if (RightValue == 1)
                            ReplaceWith(Left);
                        else if (!(float.IsNaN(LeftValue) || float.IsNaN(RightValue)))
                            ReplaceWith( LeftValue * RightValue);
                        else if (Left.Value is Variable && Right.Value is Variable)
                        {
                            // x * x
                            if (Left.Value.ToString() == Right.Value.ToString())
                                ReplaceWith(Left.Value, State.GetOperator("^"), (Number)2);
                        }
                        else if (Left.Value is Exponentiation && Right.Value is Number)
                        {
                            // x^2 * x
                            if (Left.Left.Value is Number && Left.Left.Value.ToString() == Right.Value.ToString())
                            {
                                // x^2 -> x^2 + 1
                                Left.Right = new ExpressionTree(State.GetOperator("+")) { Left = Left.Right, Right = new ExpressionTree((Number)1) };
                                ReplaceWith(Left);
                            }
                        }
                        else if (Right.Value is Exponentiation && Left.Value is Number)
                        {
                            throw new NotImplementedException("a*a^b not done yet :S");
                            // x^2 * x
                            if (Right.Left.Value is Number && Right.Left.Value.ToString() == Left.Value.ToString())
                            {
                                // x^2 -> x^2 + 1
                                Right.Right = new ExpressionTree(State.GetOperator("+")) { Left = Right.Right, Right = new ExpressionTree((Number)1) };
                                ReplaceWith(Right);
                            }
                        }
                        else if (Left.Value is Multiplication)
                        {
                            // (a * b) * c
                            float LeftLeftValue = (Left.Left.Value is Number) ? ((Number)Left.Left.Value).Value : float.NaN;
                            float LeftRightValue = (Left.Right.Value is Number) ? ((Number)Left.Right.Value).Value : float.NaN;
                            if (!(float.IsNaN(RightValue) || float.IsNaN(LeftLeftValue)))
                                Left.Left.Value = (Number)(LeftLeftValue * RightValue);
                            else if (!(float.IsNaN(RightValue) || float.IsNaN(LeftRightValue)))
                                Left.Right.Value = (Number)(LeftRightValue * RightValue);
                            else break;
                            ReplaceWith(Left);
                        }
                        else if (Right.Value is Multiplication)
                        {
                            // a + (b + c)
                            float RightLeftValue = (Right.Left.Value is Number) ? ((Number)Right.Left.Value).Value : float.NaN;
                            float RightRightValue = (Right.Right.Value is Number) ? ((Number)Right.Right.Value).Value : float.NaN;
                            if (!(float.IsNaN(LeftValue) || float.IsNaN(RightLeftValue)))
                                Right.Left.Value = (Number)(RightLeftValue * LeftValue );
                            else if (!(float.IsNaN(LeftValue) || float.IsNaN(RightRightValue)))
                                Right.Right.Value = (Number)(RightRightValue * LeftValue );
                            else break;
                            ReplaceWith(Right);
                        }
                        break;
                    case "/":
                        if (LeftValue == 0)
                        {
                            ReplaceWith(0);
                        }
                        else if (RightValue == 1)
                            ReplaceWith(Left);
                        else if (!(float.IsNaN(LeftValue) || float.IsNaN(RightValue)))
                        {
                            ReplaceWith(LeftValue / RightValue);
                        }
                        break;
                    case "^":
                        if (RightValue == 1)
                            ReplaceWith(Left);
                        else if (LeftValue == 1)
                            ReplaceWith(1);
                        else if (!(float.IsNaN(LeftValue) || float.IsNaN(RightValue)))
                        {
                            ReplaceWith(Math.Pow(LeftValue,RightValue));
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (Value is MathEvaluator.MathOperations.Functions.Function)
            {
                foreach (var v in FunctionParameters)
                    v.Optimize(State);
                switch(Value.ToString())
                {
                    case "ln":
                        if(FunctionParameters[0].ToString() == "e")
                            ReplaceWith(1);
                        break;
                    default:
                        break;
                }

            }
        }
     
        public float Calculate()
        {
            return Value.Calculate(GetExpressionStack(new StaticStack<MathOperation>()));
        }

        public void BuildBinaryTree(StaticStack<MathOperation> NumStaticStack)
        {
            Value.BuildBinaryTree(NumStaticStack,this);
        }
    }
}
