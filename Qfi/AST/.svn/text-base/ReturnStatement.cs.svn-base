using System.Collections.Generic;

namespace Qfi.AST
{
    class ReturnStatement : Statement
    {
        Statement returnStatement;
        public ReturnStatement(Statement returns)
        {
            returnStatement = returns;
        }
        public override string ToString()
        {
            return "return " + returnStatement.ToString();
        }
        public override float Calculate(Dictionary<string, float> variables, Dictionary<string, Function> functions)
        {
            variables["return"] = returnStatement.Calculate(variables, functions);
            return 0;
        }
        public override void Compile(System.Reflection.Emit.ILGenerator emit)
        {
            throw new System.NotImplementedException();
        }
    }
}
