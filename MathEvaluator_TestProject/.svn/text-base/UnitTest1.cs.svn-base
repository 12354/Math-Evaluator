using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MathEvaluator;
using MathEvaluator.Exceptions;
using MathEvaluator.MathOperations;
using MathEvaluator.MathOperations.Operators;
using MathEvaluator.MathOperations.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathEvaluator_TestProject
{
    [TestClass]
    public class UnitTest1
    {
        public class ThrowsInvalidOperatorIdentifier : Operator
        {
            public override int Precedence
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
            public override Association Association
            {
                get { throw new NotImplementedException(); }
            }
            public override Number Calculate(float a, float b)
            {
                throw new NotImplementedException();
            }
            public override string ToString()
            {
                return "wrong";
            }
            public override void CopyToStack(MathEvaluator.ExpressionTree cur, MathEvaluator.StaticStack<MathOperation> DeriveStack)
            {
                throw new MathEvaluator.Exceptions.UndefinedResultException(ToString() + " is not derivable");
            }
            public override void DeriveToStack(MathEvaluator.ExpressionTree cur,MathState s, MathEvaluator.StaticStack<MathOperation> DeriveStack)
            {
                throw new MathEvaluator.Exceptions.UndefinedResultException(ToString() + " is not derivable");
            }
        }
        public class ThrowsInvalidFunctionIdentifier : Function
        {
            public override int Precedence
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
            public override Association Association
            {
                get { throw new NotImplementedException(); }
            }
            public override Number Calculate(float a, float b)
            {
                throw new NotImplementedException();
            }
            public override string ToString()
            {
                return "-";
            }
        }
        public class ThrowsInvalidFunctionArgumentCount : Function
        {
            public override int Precedence
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
            public override Association Association
            {
                get { throw new NotImplementedException(); }
            }
            public override string ToString()
            {
                return "a";
            }
        }
        MathState math = new MathState();
        string _expression;
        [TestMethod]
        public void SimpleAddition()
        {
            Given("1 + 1");
            Expect(2);
        }
        [TestMethod]
        public void OperatorPrecedence()
        {
            Given("1 + 2 * 5");
            Expect(11);
        }
        [TestMethod]
        public void EqualOperator()
        {
            Given("1 + 1 = 2");
            Expect(1);
            Given("1 + 1 = 3");
            Expect(0);
        }
        [TestMethod]
        public void NotEqualOperator()
        {
            Given("1 + 1 != 2");
            Expect(0);
            Given("1 + 1 != 3");
            Expect(1);
        }
        [TestMethod]
        public void Operators()
        {
            Given("1 + 2 - 3 * 4 / (5 % 6) ^ 10");
            Expect(2.999f);
        }
        [TestMethod]
        public void Sin()
        {
            Given("sin(0.5)");
            Expect(0.479f);
        }
        [TestMethod]
        public void Cos()
        {
            Given("cos(0.5)");
            Expect(0.877f);
        }
        [TestMethod]
        public void Acos()
        {
            Given("acos(0.5)");
            Expect(1.047f);
        }
        [TestMethod]
        public void Asin()
        {
            Given("asin(0.5)");
            Expect(0.523f);
        }
        [TestMethod]
        public void Abs()
        {
            Given("abs(0.5)");
            Expect(0.5f);
            Given("abs(-0.5)");
            Expect(0.5f);
        }
        [TestMethod]
        public void Log()
        {
            Given("log(10^2)");
            Expect(2);
        }
        [TestMethod]
        public void Ln()
        {
            Given("ln(e^2)");
            Expect(2);
        }
        [TestMethod]
        public void Sign()
        {
            Given("sign(3)");
            Expect(1);
            Given("sign(0)");
            Expect(0);
            Given("sign(-3)");
            Expect(-1);
        }
        [TestMethod]
        public void Sqrt()
        {
            Given("sqrt(4)");
            Expect(2);
            Given("sqrt(-4)");
            Expect(float.NaN);
        }
        [TestMethod]
        public void Pow()
        {
            Given("pow(5,2)");
            Expect(25);
        }
        [TestMethod]
        public void tan()
        {
            Given("tan(0.5)");
            Expect(0.546f);
        }
        [TestMethod]
        public void NegativeNumbers()
        {
            Given("-5");
            Expect(-5);
            Given("(-5+3)");
            Expect(-2);
            Given("-5*(-5+3)");
            Expect(10);
        }
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivisionByZeroException()
        {
            Given("1/0");
            Expect(0);
        }
        [TestMethod]
        public void DefaultVariables()
        {
            Given("pi");
            Expect(3.141f);
            Given("tao");
            Expect(6.283f);
            Given("e");
            Expect(2.718f);
        }
        [TestMethod]
        public void CustomVariables()
        {
            Given("sin(x)*y");
            math.SetVariable("x", 1);
            math.SetVariable("y", 0.5f);
            Expect(0.420f);
        }
        [TestMethod]
        public void AddDefaultMultiplicator()
        {
            math.SetVariable("x", 10);
            Given("5x");
            Expect(50);

            Given("5sin(x)");
            Expect(-2.720f);

            Given("5(x)");
            Expect(50);
        }
        [TestMethod]
        public void CustomFunctions()
        {
            math.AddCustomFunction("testA", x => x * x);
            Console.WriteLine("testA => x * x");
            math.AddCustomFunction("testB", (x,y) => x * y);
            Console.WriteLine("testB => x * y");
            math.AddCustomFunction("testC", (x,y,z) => x * y + z);
            Console.WriteLine("testC => x * y + z");
            math.AddCustomFunction("testD", (x,y,z,a) => x * y * z + a);
            Console.WriteLine("testD => x * y * z + a");
            Given("testA(4)");
            Expect(16);
            Given("testB(4,5)");
            Expect(20);
            Given("testC(4,5,1)");
            Expect(21);
            Given("testD(4,5,5,1)");
            Expect(101);
        }

        [TestMethod]
        [ExpectedException(typeof(UndefinedFunctionException))]
        public void UndefinedFunctionException()
        {
            math.DefineUnknownVariables = false;
            Given("undefined(4)");

            Expect(0);
            math.DefineUnknownVariables = true;
        }
        [TestMethod]
        [ExpectedException(typeof(UndefinedVariableException))]
        public void UndefinedVariableException()
        {
            math.DefineUnknownVariables = true;
            Given("undefined + 4");

            Expect(0);
        }
        [TestMethod]
        [ExpectedException(typeof(MismatchedParenthesisException))]
        public void OpeningParenthesisException()
        {
            math.DefineUnknownVariables = true;
            Given("1 + 2)");

            Expect(0);
        }
        [TestMethod]
        [ExpectedException(typeof(MismatchedParenthesisException))]
        public void ClosingParenthesisException()
        {
            math.DefineUnknownVariables = true;
            Given("(1 + 2");

            Expect(0);
        }
        /*[TestMethod]
        [ExpectedException(typeof(NotEnoughArgumentsException))]
        public void ArgumentNotFoundException()
        {
            Given("5 +");

            Expect(0);
        }*/
        [TestMethod]
        [ExpectedException(typeof(InvalidOperatorIdentifierException))]
        public void InvalidOperatorIdentifierException()
        {
            math.AddSingleOperatorOrFunction(typeof(ThrowsInvalidOperatorIdentifier));   
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidFunctionIdentifierException))]
        public void InvalidFunctionIdentifierException()
        {
            math.AddSingleOperatorOrFunction(typeof(ThrowsInvalidFunctionIdentifier));
        }
        /*[TestMethod]
        [ExpectedException(typeof(InvalidFunctionArgumentCountException))]
        public void InvalidFunctionArgumentCountException()
        {
            math.AddSingleOperatorOrFunction(typeof(ThrowsInvalidFunctionArgumentCount));
        }*/
        
        public void Given(string expression)
        {
            _expression = expression;
        }
        private float Truncate(float result, int digits)
        {
            float multiplier = (float)Math.Pow(10.0, digits);
            return (float)Math.Truncate(result * multiplier) / multiplier;
        }
        public void Expect(float result)
        {
            MathExpression Expression = math.CreateExpression(_expression);
            Expect(Expression, result);
        }
        public void Expect(MathExpression custom, float expected)
        {
            float result = Truncate(custom.Calculate(), 3);
            Assert.AreEqual(expected, result);
            Console.WriteLine("Given: " + custom.OriginalExpression);
            Console.WriteLine("Expected: " + expected);
            Console.WriteLine("Result: " + result);
        }
        
    }
}
