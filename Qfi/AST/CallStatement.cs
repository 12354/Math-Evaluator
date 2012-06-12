using System;
using System.Collections.Generic;
using System.Linq;

namespace Qfi.AST
{
    class CallStatement : Statement
    {
        public string FunctionName { get; private set; }
        private StatementList Arguments;
        public CallStatement(string name, StatementList args)
        {
            FunctionName = name;
            Arguments = args;
        }
        public override string ToString()
        {
            return string.Format("{0}({1})", FunctionName, String.Join(",", Arguments.Select(x => x.ToString())));
        }
        public override float Calculate(Dictionary<string, float> variables, Dictionary<string, Function> functions)
        {
            Function function = functions[FunctionName];
            Dictionary<string, float> local = new Dictionary<string, float>();
            foreach (var v in variables)
            {
                local[v.Key] = v.Value;
            }
            for (int i = 0; i < Arguments.Count; i++)
            {
                local[function.Prototype.Arguments[i]] = Arguments[i].Calculate(variables, functions);
            }
            return function.Calculate(local, functions);

        }
        public override void Compile(System.Reflection.Emit.ILGenerator emit)
        {
            throw new NotImplementedException();
        }
    }
}
