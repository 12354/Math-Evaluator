using System.Collections.Generic;

namespace Qfi.AST
{
    class WhileStatement : Statement
    {
        Statement Condition;
        StatementList Body;
        public WhileStatement(Statement Condition, StatementList Body)
        {
            this.Condition = Condition;
            this.Body = Body;
        }
        public override float Calculate(Dictionary<string, float> variables, Dictionary<string, Function> functions)
        {
            while (Condition.Calculate(variables, functions) > 0)
                Body.Calculate(variables, functions);
            return 0;
        }
        public override string ToString()
        {
            return string.Format("while({0})\n then {1}", Condition, Body);
        }
        public override void Compile(System.Reflection.Emit.ILGenerator emit)
        {
            throw new System.NotImplementedException();
        }
    }
}
