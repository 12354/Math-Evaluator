using System;
using System.Collections.Generic;
using MathEvaluator.MathOperations.Operators;
namespace MathEvaluator
{
    namespace MathOperations
    {
        namespace Functions
        {
            public delegate Number FunctionDelegate1(float a);
            public delegate Number FunctionDelegate2(float a,float b);
            public delegate Number FunctionDelegate3(float a,float b,float c);
            public delegate Number FunctionDelegate4(float a,float b,float c,float d);
            public abstract class Function : MathOperation
            {
                public override int Precedence
                {
                    get
                    {
                        return -1;
                    }
                }
                public virtual int ArgumentCount { get; set; }
                public override Association Association
                {
                    get { return Association.None; }
                }
                public override Number Calculate(StaticStack<MathOperation> NumStaticStack)
                {
                    float a, b, c, d;
                    if (NumStaticStack.Count < ArgumentCount)
                    {
                        throw new Exceptions.NotEnoughArgumentsException("Not enough arguments for \"" + ToString() +"\"");
                    }
                    switch (ArgumentCount)
                    {
                       case 0:
                            return Calculate();
                        case 1:
                            return Calculate(NumStaticStack.Pop().Calculate(NumStaticStack));
                        case 2:
                            b = NumStaticStack.Pop().Calculate(NumStaticStack);
                            a = NumStaticStack.Pop().Calculate(NumStaticStack);
                            return Calculate(a,b);
                        case 3:
                            c = NumStaticStack.Pop().Calculate(NumStaticStack);
                            b = NumStaticStack.Pop().Calculate(NumStaticStack);
                            a = NumStaticStack.Pop().Calculate(NumStaticStack);
                            return Calculate(a,b,c);
                        case 4:
                            d = NumStaticStack.Pop().Calculate(NumStaticStack);
                            c = NumStaticStack.Pop().Calculate(NumStaticStack);
                            b = NumStaticStack.Pop().Calculate(NumStaticStack);
                            a = NumStaticStack.Pop().Calculate(NumStaticStack);
                            return Calculate(a,b,c,d);
                        default:
                            throw new NotSupportedException("ArgumentCount not supported!");
                    }
                }
                public virtual void CopyDerivedFunctionToStack(ExpressionTree cur,ExpressionTree inner, StaticStack<MathOperation> DeriveStack,MathState State)
                {
                    throw new MathEvaluator.Exceptions.UndefinedResultException(ToString() + " is not derivable");
                }
                public virtual Number Calculate() { throw new NotImplementedException(); }
                public virtual Number Calculate(float a) { throw new NotImplementedException(); }
                public virtual Number Calculate(float a, float b) { throw new NotImplementedException(); }
                public virtual Number Calculate(float a, float b, float c) { throw new NotImplementedException(); }
                public virtual Number Calculate(float a, float b, float c, float d) { throw new NotImplementedException(); }
                public override void BuildBinaryTree(StaticStack<MathOperation> NumStaticStack,  ExpressionTree b)
                {
                    b.Value = this;                   
                    b.FunctionParameters = new List<ExpressionTree>();
                    
                    for(int i = 0;i< ArgumentCount;i++)
                    {
                        b.FunctionParameters.Add(new ExpressionTree(NumStaticStack));
                        //b.FunctionParameters[0].Value.BuildBinaryTree(NumStaticStack,b.FunctionParameters[0]);
                    }
                }
                public override void CopyToStack(ExpressionTree cur, StaticStack<MathOperation> Stack)
                {
                    foreach (var b in cur.FunctionParameters)
                        b.CopyToStack(Stack);
                    Stack.Push(this);
                }
                public override void DeriveToStack(ExpressionTree cur, MathState State, StaticStack<MathOperation> DeriveStack)
                {
                    //f(x) = u(v(x))
                    //f'(x) = u'(v(x))*v'(x)
                    // v u' v' *
                    if(ArgumentCount != 1)
                        throw new MathEvaluator.Exceptions.UndefinedResultException(ToString() + " is not derivable");
                    ExpressionTree inner = cur.FunctionParameters[0];
                    
                    CopyDerivedFunctionToStack(cur,inner, DeriveStack,State);
                    inner.DeriveToStack(DeriveStack,State);
                    DeriveStack.Push(new Multiplication());
                }
                public abstract override string ToString();
            }
            class Random : Function
            {
                public static System.Random rnd = new System.Random();
                public override Number Calculate()
                {
                    return rnd.NextDouble();
                }
                public override string ToString()
                {
                    return "rnd";
                }
            }
            class Pow : Function
            {
                public override Number Calculate(float a,float b)
                {
                    return Math.Pow(a,b);
                }
                public override string ToString()
                {
                    return "pow";
                }
            }
            class InverseCosinus : Function
            {
                public override Number Calculate(float a)
                {
                    return Math.Acos(a);
                }
                public override string ToString()
                {
                    return "acos";
                }
            }
            class Sinus : Function
            {
                public override Number Calculate(float a)
                {
                    return Math.Sin(a);
                }
                public override string ToString()
                {
                    return "sin";
                }
                public override void CopyDerivedFunctionToStack(ExpressionTree cur,ExpressionTree inner, StaticStack<MathOperation> DeriveStack,MathState State)
                {
                    inner.CopyToStack(DeriveStack);
                    DeriveStack.Push(State.GetFunction("cos"));
                }
            }
            class Cosinus : Function
            {
                public override Number Calculate(float a)
                {
                    return Math.Cos(a);
                }
                public override string ToString()
                {
                    return "cos";
                }
                public override void CopyDerivedFunctionToStack(ExpressionTree cur, ExpressionTree inner, StaticStack<MathOperation> DeriveStack,MathState State)
                {
                    inner.CopyToStack(DeriveStack);
                    DeriveStack.Push(State.GetFunction("sin"), (Number)(-1), State.GetOperator("*"));
                }
            }
            class Sign : Function
            {
                public override Number Calculate(float a)
                {
                    return Math.Sign(a);
                }
                public override string ToString()
                {
                    return "sign";
                }
            }
            class Tangent : Function
            {
                public override Number Calculate(float a)
                {
                    return Math.Tan(a);
                }
                public override string ToString()
                {
                    return "tan";
                }
            }
            class InverseSinus : Function
            {
                public override Number Calculate(float a)
                {
                    return Math.Asin(a);
                }
                public override string ToString()
                {
                    return "asin";
                }
            }
            class SquareRoot : Function
            {
                public override Number Calculate(float a)
                {
                    return Math.Sqrt(a);
                }
                public override string ToString()
                {
                    return "sqrt";
                }
                public override void CopyDerivedFunctionToStack(ExpressionTree cur, ExpressionTree inner, StaticStack<MathOperation> DeriveStack, MathState State)
                {
                    //sqrt(x)' = 1/(2*sqrt(x))
                    // 1 2 x sqrt * /
                    DeriveStack.Push((Number)1,(Number)2);
                    inner.CopyToStack(DeriveStack);
                    DeriveStack.Push(State.GetFunction("sqrt"), State.GetOperator("*"), State.GetOperator("/"));
                }
                
            }
            class Absolute : Function
            {
                public override Number Calculate(float a)
                {
                    return Math.Abs(a);
                }
                public override string ToString()
                {
                    return "abs";
                }
            }
            class LogarithmBase10 : Function
            {
                public override Number Calculate(float a)
                {
                    if (a == 0)
                        throw new Exceptions.UndefinedFunctionException("log(0) is not defined");
                    return Math.Log10(a);
                }
                public override string ToString()
                {
                    return "log";
                }
                public override void CopyDerivedFunctionToStack(ExpressionTree cur, ExpressionTree inner, StaticStack<MathOperation> DeriveStack, MathState State)
                {
                    //sqrt(x)' = 1/(x * ln 10)
                    // 1 x 10 ln * / 

                    DeriveStack.Push((Number)1);
                    inner.CopyToStack(DeriveStack);
                    DeriveStack.Push((Number)10,State.GetFunction("ln"),State.GetOperator("*"), State.GetOperator("/"));
                }
            }
            class NaturalLogarithm : Function
            {
                public override Number Calculate(float a)
                {
                    return Math.Log(a);
                }
                public override string ToString()
                {
                    return "ln";
                }
                public override void CopyDerivedFunctionToStack(ExpressionTree cur, ExpressionTree inner, StaticStack<MathOperation> DeriveStack, MathState State)
                {
                    //ln(x)' = 1/x
                    // 1 x /
                    DeriveStack.Push((Number)1);
                    inner.CopyToStack(DeriveStack);
                    DeriveStack.Push(State.GetOperator("/"));
                }
            }
          
            public class CustomFunction : Function
            {
                private string _name;
                //stupid solution :(
                private FunctionDelegate1 _calculate1;
                private FunctionDelegate2 _calculate2;
                private FunctionDelegate3 _calculate3;
                private FunctionDelegate4 _calculate4;
                public override int ArgumentCount { get { return _argumentCount; } set { } }
                int _argumentCount;
                public CustomFunction(string name, FunctionDelegate1 function)
                {
                    _name = name;
                    _calculate1 = function;
                    _argumentCount = 1;
                }
                public CustomFunction(string name, FunctionDelegate2 function)
                {
                    _name = name;
                    _calculate2 = function;
                    _argumentCount = 2;
                }
                public CustomFunction(string name, FunctionDelegate3 function)
                {
                    _name = name;
                    _calculate3 = function;
                    _argumentCount = 3;
                }
                public CustomFunction(string name, FunctionDelegate4 function)
                {
                    _name = name;
                    _calculate4 = function;
                    _argumentCount = 4;
                }
                public override Number Calculate(StaticStack<MathOperation> NumStaticStack)
                {
                     float a, b, c, d;
                    if (NumStaticStack.Count < ArgumentCount)
                    {
                        throw new Exceptions.NotEnoughArgumentsException("Not enough arguments for \"" + ToString() +"\"");
                    }
                    switch (ArgumentCount)
                    {
                        case 1:
                            return _calculate1(NumStaticStack.Pop().Calculate(NumStaticStack));
                        case 2:
                            b = NumStaticStack.Pop().Calculate(NumStaticStack);
                            a = NumStaticStack.Pop().Calculate(NumStaticStack);
                            return _calculate2(a, b);
                        case 3:
                            c = NumStaticStack.Pop().Calculate(NumStaticStack);
                            b = NumStaticStack.Pop().Calculate(NumStaticStack);
                            a = NumStaticStack.Pop().Calculate(NumStaticStack);
                            return _calculate3(a, b, c);
                        case 4:
                            d = NumStaticStack.Pop().Calculate(NumStaticStack);
                            c = NumStaticStack.Pop().Calculate(NumStaticStack);
                            b = NumStaticStack.Pop().Calculate(NumStaticStack);
                            a = NumStaticStack.Pop().Calculate(NumStaticStack);
                            return _calculate4(a, b, c, d);
                        default:
                            throw new NotSupportedException("ArgumentCount not supported!");
                    }
                }
                public override string ToString()
                {
                    return _name;
                }
            }
        }
    }
}