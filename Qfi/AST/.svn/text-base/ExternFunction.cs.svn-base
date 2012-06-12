using System;
using System.Collections.Generic;

namespace Qfi.AST
{
    public class ExternFunction : Function
    {
        public delegate float CalculateDelegate();
        public delegate float CalculateDelegate1(float a);
        public delegate float CalculateDelegate2(float a, float b);
        public delegate float CalculateDelegate3(float a, float b, float z);
        CalculateDelegate function;
        CalculateDelegate1 function1;
        CalculateDelegate2 function2;
        CalculateDelegate3 function3;
        int count;
        public ExternFunction(FunctionPrototype proto, CalculateDelegate func)
            : base(proto, null)
        {
            function = func;
            count = 0;
        }
        public ExternFunction(FunctionPrototype proto, CalculateDelegate1 func)
            : base(proto, null)
        {
            function1 = func;
            count = 1;
        }
        public ExternFunction(FunctionPrototype proto, CalculateDelegate2 func)
            : base(proto, null)
        {
            count = 2;
            function2 = func;
        }
        public ExternFunction(FunctionPrototype proto, CalculateDelegate3 func)
            : base(proto, null)
        {
            count = 3;
            function3 = func;
        }
        public override float Calculate(Dictionary<string, float> variables, Dictionary<string, Function> functions)
        {
            switch (count)
            {
                case 0:
                    return function();
                case 1:
                    return function1(variables[Prototype.Arguments[0]]);
                case 2:
                    return function2(variables[Prototype.Arguments[0]], variables[Prototype.Arguments[1]]);
                case 3:
                    return function3(variables[Prototype.Arguments[0]], variables[Prototype.Arguments[1]], variables[Prototype.Arguments[2]]);
                default:
                    throw new Exception();
            }
        }
    }
}
