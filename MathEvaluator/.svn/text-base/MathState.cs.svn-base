using System;
using System.Collections.Generic;
using System.Reflection;
using MathEvaluator.Exceptions;
using MathEvaluator.MathOperations;
using MathEvaluator.MathOperations.Functions;
using MathEvaluator.MathOperations.Operators;
namespace MathEvaluator
{
    public class MathState
    {      
        enum NumberState
        {
            None,
            Number,
            Comma
        }
        public bool DefineUnknownVariables{get;set;}
        Dictionary<string, MathOperation> m_Operators = new Dictionary<string, MathOperation>();
        Dictionary<string, MathOperation> m_Functions = new Dictionary<string, MathOperation>();
        Dictionary<string, Number> m_Constants = new Dictionary<string, Number>();
        //Dictionary<string, Variable> m_Variables = new Dictionary<string,Variable>();
        Dictionary<string, float> m_Variables = new Dictionary<string, float>();
        public List<MathOperation> Functions
        {
            get
            {
                return new List<MathOperation>(m_Functions.Values);
            }
        }
        public MathState()
        {
            DefineUnknownVariables = true;
            m_Constants["pi"] = (float)Math.PI;// new Variable("PI", Math.PI);
            m_Constants["e"] = (float)Math.E; // new Variable("E", Math.E);
            m_Constants["tao"] = (float)Math.PI * 2; // new Variable("Tao", Math.PI * 2.0);
            AddOperatorsAndFunctions("MathEvaluator.MathOperations.Functions");
            AddOperatorsAndFunctions("MathEvaluator.MathOperations.Operators");
        }
        public void AddCustomFunction(string name, FunctionDelegate1 function)
        {
            string lowername = name.ToLower();
            if (m_Functions.ContainsKey(lowername))
                throw new FunctionAlreadyDefinedException("Function " + name + " already defined!");
            m_Functions[lowername] = new CustomFunction(lowername, function);
        }
        public void AddCustomFunction(string name, FunctionDelegate2 function)
        {
            string lowername = name.ToLower();
            if (m_Functions.ContainsKey(lowername))
                throw new FunctionAlreadyDefinedException("Function " + name + " already defined!");
            m_Functions[lowername] = new CustomFunction(lowername, function);
        }
        public void AddCustomFunction(string name, FunctionDelegate3 function)
        {
            string lowername = name.ToLower();
            if (m_Functions.ContainsKey(lowername))
                throw new FunctionAlreadyDefinedException("Function " + name + " already defined!");
            m_Functions[lowername] = new CustomFunction(lowername, function);
        }
        public void AddCustomFunction(string name, FunctionDelegate4 function)
        {
            string lowername = name.ToLower();
            if (m_Functions.ContainsKey(lowername))
                throw new FunctionAlreadyDefinedException("Function " + name + " already defined!");
            m_Functions[lowername] = new CustomFunction(lowername, function);
        }
        public void SetVariable(string variable, float value)
        {
            string lowername = variable.ToLower();
            m_Variables[lowername] = value; ;
        }
        public float GetVariable(string variable)
        {
            string lowername = variable.ToLower();
            if(m_Variables.ContainsKey(lowername))
            {
                return m_Variables[lowername];
            }
            throw new UndefinedVariableException(variable + " is not defined");
        }
        public MathOperation GetFunction(string name)
        {
            string lowername = name.ToLower();
            if (m_Functions.ContainsKey(lowername))
                return m_Functions[lowername];
            else
                throw new UndefinedFunctionException(name + " is not defined");
        }
        public MathOperation GetOperator(string name)
        {
            string lowername = name.ToLower();
            if (m_Operators.ContainsKey(lowername))
                return m_Operators[lowername];
            else
                throw new UndefinedFunctionException(name + " is not defined");
        }
        private int DetermineArgumentCount(Function function)
        {
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    switch (i)
                    {
                        case 0:
                            function.Calculate();
                            break;
                        case 1:
                            function.Calculate(1);
                            break;
                        case 2:
                            function.Calculate(1, 1);
                            break;
                        case 3:
                            function.Calculate(1, 1, 1);
                            break;
                        case 4:
                            function.Calculate(1, 1, 1, 1);
                            break;
                    }
                    return i;
                }
                catch (NotImplementedException)
                {
                }
            }
            return 0;

        }
        public void AddSingleOperatorOrFunction(Type type)
        {
            Type Operator = typeof(Operator);
            Type Function = typeof(Function);
            if (type.BaseType == Function)
            {
                //Get constructors without parameters
                ConstructorInfo info = type.GetConstructor(new Type[0]);
                if (info != null)
                {
                    Function func = (Function)Activator.CreateInstance(type);
                    foreach (char c in func.ToString())
                    {
                        if (!char.IsLetter(c))
                            throw new InvalidFunctionIdentifierException("Function identifier '" + func.ToString() + "' not supported('" + c + "' not supported).\nOnly letters are supported as function identifiers.");
                    }
                    func.ArgumentCount = DetermineArgumentCount(func);
                    if (func.ArgumentCount < 0 || func.ArgumentCount > 4)
                        throw new InvalidFunctionArgumentCountException("Function '" + func.ToString() + "':\n" + func.ArgumentCount + " arguments not supported!");
                    m_Functions[func.ToString()] = func;
                }
            }
            else if (type.BaseType == Operator)
            {
                //Get constructors without parameters
                ConstructorInfo info = type.GetConstructor(new Type[0]);
                if (info != null)
                {
                    MathOperation Op = (MathOperation)Activator.CreateInstance(type);
                    if (Op.ToString().Length == 0)
                        throw new NotSupportedException();
                    foreach (char c in Op.ToString())
                    {
                        if (!char.IsSymbol(c) && !char.IsPunctuation(c))
                            throw new InvalidOperatorIdentifierException("Operator identifier '" + Op.ToString() + "' not supported('" + c + "' not supported).\nOnly symbols and punctuation marks are supported as operator identifiers.");
                    }
                    if (Op.Precedence >= 0)
                    {
                        m_Operators[Op.ToString()] = Op;
                    }
                }

            }
        }

        public void AddOperatorsAndFunctions(string Namespace)
        {
            Type Operator = typeof(Operator);
            Type Function = typeof(Function);
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.Namespace != null && type.Namespace.StartsWith(Namespace))
                {
                    AddSingleOperatorOrFunction(type);
                }
                    
            }
        }
        public string[] GetOperators()
        {
            List<string> OperatorList = new List<string>(m_Functions.Keys);
            OperatorList.AddRange(m_Operators.Keys);
            return OperatorList.ToArray();
        }
        private float NonNullSign(float a)
        {
            if (a >= 0)
                return 1;
            return -1;
        }
        private char LastToken(string expression,int index)
        {
            if (index == 0)
                return '\0';
            for (int i = index - 1; i >= 0; i--)
            {
                char current = expression[i];
                if (!char.IsWhiteSpace(current))
                    return current;
            }
            return '\0';
        }
        public StaticStack<MathOperation> Convert(string expression)
        {
            StaticStack<MathOperation> Output = new StaticStack<MathOperation>();
            Stack<MathOperation> OperatorStack = new Stack<MathOperation>();
            NumberState NumberState = NumberState.None;

            float CommaMultiplier = 0;
            float minus = 1;
            bool useMinus = true;
            for (int i = 0; i < expression.Length; i++)
            {
                char token = expression[i];
                if (char.IsWhiteSpace(token))
                {
                    //fixes 5 2 = 52
                    //new bug: 5 2 + = 7 (infix)
                    NumberState = NumberState.None;
                    continue;
                }
                if(token == '-')
                {
                    if (useMinus)
                    {
                        minus = -1;
                        continue;
                    }
                }
                if (char.IsDigit(token))
                {
                    float numeric = (float)char.GetNumericValue(token);
                    switch (NumberState)
                    {
                        case NumberState.None:
                            Output.Push((Number)(numeric * minus));
                            NumberState = NumberState.Number;
                            break;
                        case NumberState.Number:
                            float num = (Number)Output.PopReal();
                            Output.Push((Number)(num * 10.0 + numeric * NonNullSign(num)));
                            break;
                        case NumberState.Comma:
                            float num2 = (Number)Output.PopReal();
                            Output.Push((Number)(num2 + numeric * CommaMultiplier * NonNullSign(num2)));
                            CommaMultiplier *= 0.1f;
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    minus = 1;
                    useMinus = false;
                }
                else if (token == '.')
                {
                    minus = 1;
                    NumberState = NumberState.Comma;
                    CommaMultiplier = 0.1f;
                }
                else if (token == ',')
                {
                    NumberState = NumberState.None;

                    if (OperatorStack.Count == 0)
                        throw new MismatchedParenthesisException("\"" + expression.Substring(i) + "\": Can't find opening parenthesis");
                    while (OperatorStack.Count > 0)
                    {
                        if (OperatorStack.Peek() is Parenthesis)
                            break;
                        Output.Push(OperatorStack.Pop());
                    }
                    if (OperatorStack.Count == 0 || !(OperatorStack.Peek() is Parenthesis))
                        throw new MismatchedParenthesisException("\"" + expression.Substring(i) + "\": Can't find opening parenthesis");

                }
                else if (token == ')')
                {
                    if (OperatorStack.Count == 0)
                        throw new MismatchedParenthesisException("\"" + expression.Substring(i) + "\": Can't find opening parenthesis");
                    while (OperatorStack.Count > 0)
                    {
                        MathOperation TopOperator = OperatorStack.Pop();
                        if (TopOperator is Parenthesis)
                            break;
                        else
                            Output.Push(TopOperator);
                        if (OperatorStack.Count == 0)
                            throw new MismatchedParenthesisException("\"" + expression.Substring(i) + "\": Can't find opening parenthesis");
                    }
                    if (OperatorStack.Count > 0 && OperatorStack.Peek() is Function)
                        Output.Push(OperatorStack.Pop());
                    useMinus = false;
                }
                else if (char.IsSymbol(token) || char.IsPunctuation(token))
                {
                    string fullname = "";
                    for (; i < expression.Length; i++)
                    {
                        char letter = expression[i];
                        if ((char.IsSymbol(letter) || char.IsPunctuation(letter)))
                            //we cant use letters or 5 + sin(x) is going to break
                            fullname += char.ToLower(letter);
                        else
                        {
                            --i;
                            break;
                        }
                        if (m_Operators.ContainsKey(fullname))
                            break;
                    }
                    if (m_Operators.ContainsKey(fullname))
                    {
                        MathOperation TokenOperator = m_Operators[fullname];
                        if (NumberState != MathState.NumberState.None)
                        {
                            NumberState = NumberState.None;
                            if (TokenOperator is Parenthesis)
                            {
                                // makes 5(x) possible
                                OperatorStack.Push(new MathOperations.Operators.Multiplication());
                            }
                        }
                        while (OperatorStack.Count > 0)
                        {
                            MathOperation TopOperator = OperatorStack.Peek();
                            if (TokenOperator.Association == Association.Left && TokenOperator.Precedence <= TopOperator.Precedence)
                                Output.Push(OperatorStack.Pop());
                            else if (TokenOperator.Association == Association.Right && TokenOperator.Precedence < TopOperator.Precedence)
                                Output.Push(OperatorStack.Pop());
                            else
                                break;
                        }
                        OperatorStack.Push(TokenOperator);
                    }
                    useMinus = true;
                }
                else if (char.IsLetter(token))
                {
                    minus = 1;
                    string fullname = "";
                    for (; i < expression.Length; i++)
                    {
                        char letter = expression[i];
                        if (char.IsLetter(letter))
                            fullname += char.ToLower(letter);
                        else
                        {
                            --i;
                            break;
                        }
                    }
                    if (NumberState != MathState.NumberState.None)
                    {
                        NumberState = NumberState.None;
                        OperatorStack.Push(new MathOperations.Operators.Multiplication());
                    }
                    if (m_Functions.ContainsKey(fullname))
                        OperatorStack.Push(m_Functions[fullname]);
                    else if (m_Variables.ContainsKey(fullname))
                    {
                        Output.Push(new Variable(fullname, m_Variables));
                    }
                    else if (m_Constants.ContainsKey(fullname))
                    {
                        Output.Push(new Constant(fullname, m_Constants[fullname]));
                    }
                    else if (DefineUnknownVariables)
                        Output.Push(new Variable(fullname,m_Variables));
                    else
                        throw new UndefinedFunctionException("Function \"" + fullname + "\" not defined");
                    useMinus = false;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            while (OperatorStack.Count > 0)
            {
                if (OperatorStack.Peek() is Parenthesis)
                    throw new MismatchedParenthesisException("Can't find closing parenthesis");
                Output.Push(OperatorStack.Pop());
            }
            return Output;
        }
        public MathExpression CreateExpression(string expression)
        {
            StaticStack<MathOperation> Expression = Convert(expression);
            //the new MathExpression gets the original
            Dictionary<string, float> var = m_Variables;
            //and we keep a copy so they dont intefere with eachother
            m_Variables = new Dictionary<string, float>(var);

            return new MathExpression(Expression,var,expression);

        }
        public MathExpression CreateExpression(StaticStack<MathOperation> expression)
        {
            expression.ResetTop();
            StaticStack<MathOperation> Expression = expression;
            //the new MathExpression gets the original
            Dictionary<string, float> var = m_Variables;
            //and we keep a copy so they dont intefere with eachother
            m_Variables = new Dictionary<string, float>(var);

            return new MathExpression(Expression, var, "created");
        }
        public bool TryCreateExpression(string expression,out MathExpression ex)
        {
            try
            {
                StaticStack<MathOperation> Expression = Convert(expression);
                //the new MathExpression gets the original
                Dictionary<string, float> var = m_Variables;
                //and we keep a copy so they dont intefere with eachother
                m_Variables = new Dictionary<string, float>(var);
                ex = new MathExpression(Expression, var, expression);
                return true;
            }
            catch (Exception)
            {
                ex = null;
                return false;
            }
  
        }
    }

}
