using System;
using System.Collections.Generic;

using MathEvaluator.Exceptions;
namespace MathEvaluator
{
    namespace MathOperations
    {
        public enum Association
        {
            Left,
            Right,
            None
        }

        public abstract class MathOperation
        {

            public abstract int Precedence { get; }
            public abstract Association Association { get; }
            public abstract Number Calculate(StaticStack<MathOperation> NumStaticStack);
            public abstract void BuildBinaryTree(StaticStack<MathOperation> NumStaticStack,ExpressionTree b);
            public abstract void DeriveToStack(ExpressionTree cur,MathState State,StaticStack<MathOperation> DeriveStack);
            public abstract void CopyToStack(ExpressionTree cur, StaticStack<MathOperation> Stack);
        }

        public class Number : MathOperation
        {
            public float Value { get; set; }
            public Number(float i)
            {
                Value = i;
            }
            public static implicit operator Number(float d)
            {
                return new Number(d);
            }
            public static implicit operator Number(double d)
            {
                return new Number((float)d);
            }
            public static implicit operator float(Number d)
            {
                return d.Value;
            }
            public static implicit operator double(Number d)
            {
                return d.Value;
            }
            public override int Precedence
            {
                get
                {
                    return 0;
                }
            }
            public override Association Association
            {
                get { return Association.None; }
            }
            public override Number Calculate(StaticStack<MathOperation> NumStaticStack)
            {
                return Value;
            }
            public override string ToString()
            {
                return Value.ToString();
            }
            public override void BuildBinaryTree(StaticStack<MathOperation> NumStaticStack,ExpressionTree b)
            {
                b.Value = this;
            }
            public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
            {
                DeriveStack.Push((Number)0);
            }
            public override void CopyToStack(ExpressionTree cur, StaticStack<MathOperation> Stack)
            {
                Stack.Push(this);
            }
        }
        public class Constant : Number
        {
            string name;
            public Constant(string name, float value)
                : base(value)
            {
                this.name = name;
            }
            public override string ToString()
            {
                return name;
            }
        }

        namespace Operators
        {

            public abstract class Operator : MathOperation
            {
                public abstract Number Calculate(float da, float b);
                public override Number Calculate(StaticStack<MathOperation> NumStaticStack)
                {
                    if (NumStaticStack.Count < 2)
                        throw new NotEnoughArgumentsException("Operator \"" + ToString() + "\": not enough arguments");
                    float b = NumStaticStack.Pop().Calculate(NumStaticStack);
                    float a = NumStaticStack.Pop().Calculate(NumStaticStack);
                    return Calculate(a, b);
                }
                public override void BuildBinaryTree(StaticStack<MathOperation> NumStaticStack, ExpressionTree b)
                {
                    b.Right = new ExpressionTree(NumStaticStack);
                    b.Left = new ExpressionTree(NumStaticStack);
                    b.Value = this;
                }
                public override void CopyToStack(ExpressionTree cur, StaticStack<MathOperation> Stack)
                {
                    cur.Left.CopyToStack(Stack);
                    cur.Right.CopyToStack(Stack);
                    Stack.Push(this);
                }
                public virtual bool IsLogical { get { return false; } }
            }
            class Minus : Operator
            {
                public override int Precedence
                {
                    get
                    {
                        return 2;
                    }
                }
                public override Association Association
                {
                    get { return Association.Left; }
                }
                public override Number Calculate(float a, float b)
                {
                    return a - b;
                }
                public override string ToString()
                {
                    return "-";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    cur.Left.DeriveToStack(DeriveStack, State);
                    cur.Right.DeriveToStack(DeriveStack, State);
                    DeriveStack.Push(this);
                }
                
            }
            class Modulo : Operator
            {
                public override int Precedence
                {
                    get
                    {
                        return 3;
                    }
                }
                public override Association Association
                {
                    get { return Association.Left; }
                }
                public override Number Calculate(float a, float b)
                {
                    return a % b;
                }
                public override string ToString()
                {
                    return "%";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    throw new UndefinedResultException(ToString() + " is not derivable");
                }
            }
            class Plus : Operator
            {
                public override int Precedence
                {
                    get
                    {
                        return 2;
                    }
                }
                public override Association Association
                {
                    get { return Association.Left; }
                }
                public override Number Calculate(float a, float b)
                {
                    return a + b;
                }
                public override string ToString()
                {
                    return "+";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    cur.Left.DeriveToStack(DeriveStack, State);
                    cur.Right.DeriveToStack(DeriveStack, State);
                    DeriveStack.Push(this);
                }
            }
            class Division : Operator
            {
                public override int Precedence
                {
                    get
                    {
                        return 3;
                    }
                }
                public override Association Association
                {
                    get { return Association.Left; }
                }
                public override Number Calculate(float a, float b)
                {
                    if (b == 0)
                        throw new DivideByZeroException();
                    return a / b;
                }
                public override string ToString()
                {
                    return "/";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    //(u * v)' = (u'v - uv')/v²
                    //PF:      = u' v * u v' * - v 2 ^ /
                    ExpressionTree left = cur.Left;
                    ExpressionTree right = cur.Right;

                    left.DeriveToStack(DeriveStack,State);
                    right.CopyToStack(DeriveStack);

                    DeriveStack.Push(State.GetOperator("*"));
                    left.CopyToStack(DeriveStack);
                    right.DeriveToStack(DeriveStack,State);
                    DeriveStack.Push(State.GetOperator("*"), State.GetOperator("-"));
                    right.CopyToStack(DeriveStack);
                    DeriveStack.Push((Number)2, State.GetOperator("^"), State.GetOperator("/"));
                }
            }
            class Multiplication : Operator
            {
                public override int Precedence
                {
                    get
                    {
                        return 3;
                    }
                }
                public override Association Association
                {
                    get { return Association.Left; }
                }
                public override Number Calculate(float a, float b)
                {
                    return a * b;
                }
                public override string ToString()
                {
                    return "*";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    //(u * v)' = u' * v + u * v'
                    //PF:      = u' v * u v' * +
                    ExpressionTree left = cur.Left;
                    ExpressionTree right = cur.Right;

                    left.DeriveToStack(DeriveStack,State);
                    right.CopyToStack(DeriveStack);
                    DeriveStack.Push(State.GetOperator("*"));
                    left.CopyToStack(DeriveStack);
                    right.DeriveToStack(DeriveStack,State);
                    DeriveStack.Push(State.GetOperator("*"), State.GetOperator("+"));
                }
            }
            public class Equals : Operator
            {
                public static float MAX_DIFFERENCE = 0.0001f;
                public override int Precedence
                {
                    get
                    {
                        return 0;
                    }
                }
                public override Association Association
                {
                    get { return Association.Right; }
                }
                public override Number Calculate(float a, float b)
                {
                    float dif = Math.Abs(a - b);
                /*    return dif;*/
                    return (dif < MAX_DIFFERENCE) ? 1.0 : 0.0;
                }
                public override bool IsLogical { get { return true; } }
                public override string ToString()
                {
                    return "=";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    throw new UndefinedResultException(ToString() + " is not derivable");
                }
            }
            public class LessThan : Operator
            {
                public override int Precedence
                {
                    get
                    {
                        return 0;
                    }
                }
                public override Association Association
                {
                    get { return Association.Right; }
                }
                public override Number Calculate(float a, float b)
                {
                    return (a < b) ? 1.0 : 0.0;
                }
                public override bool IsLogical { get { return true; } }
                public override string ToString()
                {
                    return "<";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    throw new UndefinedResultException(ToString() + " is not derivable");
                }
            }
            public class MoreThan : Operator
            {
                public override int Precedence
                {
                    get
                    {
                        return 0;
                    }
                }
                public override Association Association
                {
                    get { return Association.Right; }
                }
                public override Number Calculate(float a, float b)
                {
                    return (a > b) ? 1.0 : 0.0;
                }
                public override bool IsLogical { get { return true; } }
                public override string ToString()
                {
                    return ">";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    throw new UndefinedResultException(ToString() + " is not derivable");
                }
            }
            class NotEqual : Operator
            {
                public override int Precedence
                {
                    get
                    {
                        return 2;
                    }
                }
                public override Association Association
                {
                    get { return Association.Left; }
                }
                public override Number Calculate(float a, float b)
                {
                    float dif = Math.Abs(a - b);
                    return !(dif < 0.0001) ? 1.0 : 0.0;
                }
                public override string ToString()
                {
                    return "!=";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    throw new UndefinedResultException(ToString() + " is not derivable");
                }
            }
            class Xor : Operator
            {
                public override int Precedence
                {
                    get
                    {
                        return 2;
                    }
                }
                public override Association Association
                {
                    get { return Association.Left; }
                }
                public override Number Calculate(float a, float b)
                {
                    return (int)a ^ (int)b;
                }
                public override string ToString()
                {
                    return "°";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    throw new UndefinedResultException(ToString() + " is not derivable");
                }
            }
            class Exponentiation : Operator
            {
                public override int Precedence
                {
                    get
                    {
                        return 4;
                    }
                }
                public override Association Association
                {
                    get { return Association.Right; }
                }
                public override Number Calculate(float a, float b)
                {
                    return Math.Pow(a, b);
                }
                public override string ToString()
                {
                    return "^";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    // (f(x)^g(x))'
                    // f(x)^(g(x)-1) (g(x) f'(x)+f(x) ln(f(x)) g'(x))
                    // f(x) g(x) 1 - ^ g(x) f'(x) * f(x) f(x) ln * + * g'(x) *
                    //  f g 1 - ^ g v * f f ln * b * + *
                    ExpressionTree f = cur.Left;
                    ExpressionTree g = cur.Right;
                    f.CopyToStack(DeriveStack);
                    g.CopyToStack(DeriveStack);
                    DeriveStack.Push((Number)1, State.GetOperator("-"), State.GetOperator("^"));
                    g.CopyToStack(DeriveStack);
                    f.DeriveToStack(DeriveStack,State);
                    DeriveStack.Push(State.GetOperator("*"));
                    f.CopyToStack(DeriveStack);
                    f.CopyToStack(DeriveStack);
                    DeriveStack.Push(State.GetFunction("ln"), State.GetOperator("*"));
                    g.DeriveToStack(DeriveStack,State);
                    DeriveStack.Push(State.GetOperator("*"), State.GetOperator("+"), State.GetOperator("*"));



                    /*
                    if (cur.Left.ContainsVariable("x"))
                    {
                        //(x^n)' = n * x^(n-1)
                        ExpressionTree left = cur.Left;
                        ExpressionTree right = cur.Right;

                        right.CopyToStack(DeriveStack);
                        left.CopyToStack(DeriveStack);
                        right.CopyToStack(DeriveStack);
                        DeriveStack.Push((Number)1,State.GetOperator("-"),State.GetOperator("^"),State.GetOperator("*"));
                    }
                    else if (cur.Right.ContainsVariable("x"))
                    { 
                        // (a^x)' = a^x * ln a
                        // a x ^ a ln *
                        ExpressionTree left = cur.Left;
                        ExpressionTree right = cur.Right;

                        left.CopyToStack(DeriveStack);
                        right.CopyToStack(DeriveStack);
                        DeriveStack.Push(State.GetOperator("^"));

                        left.CopyToStack(DeriveStack);
                        DeriveStack.Push(State.GetFunction("ln"), State.GetOperator("*"));

                        //chain rule
                        right.DeriveToStack(DeriveStack, State);
                        DeriveStack.Push(State.GetOperator("*"));
                    }
                    else if (cur.Left.ToString() == "e" && cur.Right.ToString() == "x")
                    {
                        cur.Left.CopyToStack(DeriveStack);
                        cur.Right.CopyToStack(DeriveStack);
                        DeriveStack.Push(this);
                    }
                    else
                    {
                        DeriveStack.Push((Number)0);
                    }*/
                }
            }
            class Parenthesis : Operator
            {
                public override int Precedence
                {
                    get
                    {
                        return 0;
                    }
                }
                public override Association Association
                {
                    get { return Association.None; }
                }
                public override Number Calculate(float a, float b)
                {
                    throw new NotSupportedException();
                }
                public override string ToString()
                {
                    return "(";
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    throw new UndefinedResultException(ToString() + " is not derivable");
                }
            }
            public class Variable : MathOperation
            {
                public float Value
                {
                    get
                    {
                        string lowname = _name.ToLower();
                        if (m_Variables.ContainsKey(lowname))
                        {
                            return m_Variables[lowname];
                        }
                        throw new UndefinedVariableException("Variable \"" + _name + "\" not defined");
                    }
                }
                public Dictionary<string, float> Source { set { m_Variables = value;} }
                private Dictionary<string, float> m_Variables;
                private string _name;
                
                public Variable(string name,Dictionary<string,float> varDictionary)
                {
                    m_Variables = varDictionary;
                    _name = name;
                }
                public override int Precedence
                {
                    get
                    {
                        return 2;
                    }
                }
                public override Association Association
                {
                    get { return Association.None; }
                }
                public override Number Calculate(StaticStack<MathOperation> NumStaticStack)
                {
                    return Value;
                }
                public override string ToString()
                {
                    return _name;
                }
                public override void BuildBinaryTree(StaticStack<MathOperation> NumStaticStack,ExpressionTree b)
                {
                    b.Value = this;
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    DeriveStack.Push((Number)(_name.ToLower() == "x" ? 1 : 0));
                }
                public override void CopyToStack(ExpressionTree cur, StaticStack<MathOperation> Stack)
                {
                    Stack.Push(this);
                }
            }
        }
    }
}