using System;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace Qfi.AST
{
    class BinaryOperator : Statement
    {
        public string Operator { get; private set; }
        Statement Left;
        Statement Right;
        public BinaryOperator(string op, Statement left, Statement right)
        {
            Operator = op;
            Left = left;
            Right = right;
        }
        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Left, Operator, Right);
        }
        public override void Compile(System.Reflection.Emit.ILGenerator emit)
        {
            Left.Compile(emit);
            Right.Compile(emit);
            switch (Operator)
            {
                case "+":
                    emit.Emit(OpCodes.Add);
                    break;
                default:
                    throw new Exception();
            }
        }
        public override float Calculate(Dictionary<string, float> variables, Dictionary<string, Function> functions)
        {
            switch (Operator)
            {
                case "+":
                    return Left.Calculate(variables, functions) + Right.Calculate(variables, functions);
                case "-":
                    return Left.Calculate(variables, functions) - Right.Calculate(variables, functions);
                case "*":
                    return Left.Calculate(variables, functions) * Right.Calculate(variables, functions);
                case "/":
                    return Left.Calculate(variables, functions) / Right.Calculate(variables, functions);
                case ">":
                    return (Left.Calculate(variables, functions) > Right.Calculate(variables, functions)) ? 1.0f : 0f;
                case "<":
                    return (Left.Calculate(variables, functions) < Right.Calculate(variables, functions)) ? 1.0f : 0f;
                case "==":
                    return (Left.Calculate(variables, functions) == Right.Calculate(variables, functions)) ? 1.0f : 0f;
                case ":":
                    Left.Calculate(variables, functions);
                    return Right.Calculate(variables, functions);
                case "=":
                    if (Left is VariableStatement)
                    {
                        VariableStatement var = (VariableStatement)Left;
                        variables[var.Name] = Right.Calculate(variables, functions);
                    }
                    return Left.Calculate(variables, functions);
                case "+=":
                    if (Left is VariableStatement)
                    {
                        VariableStatement var = (VariableStatement)Left;
                        variables[var.Name] += Right.Calculate(variables, functions);
                    }
                    return Left.Calculate(variables, functions);
                case "-=":
                    if (Left is VariableStatement)
                    {
                        VariableStatement var = (VariableStatement)Left;
                        variables[var.Name] -= Right.Calculate(variables, functions);
                    }
                    return Left.Calculate(variables, functions);
                case "*=":
                    if (Left is VariableStatement)
                    {
                        VariableStatement var = (VariableStatement)Left;
                        variables[var.Name] *= Right.Calculate(variables, functions);
                    }
                    return Left.Calculate(variables, functions);
                case "/=":
                    if (Left is VariableStatement)
                    {
                        VariableStatement var = (VariableStatement)Left;
                        variables[var.Name] /= Right.Calculate(variables, functions);
                    }
                    return Left.Calculate(variables, functions);
                default:
                    throw new Exception();
            }
        }
    }
}
