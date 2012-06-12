using System.Collections.Generic;

namespace Qfi.AST
{
    class VariableStatement : Statement
    {
        public string Name { get; private set; }
        public VariableStatement(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
        public override float Calculate(Dictionary<string, float> variables, Dictionary<string, Function> functions)
        {
            return variables[Name];
        }
        public override void Compile(System.Reflection.Emit.ILGenerator emit)
        {
            throw new System.NotImplementedException();
        }
    }
}
