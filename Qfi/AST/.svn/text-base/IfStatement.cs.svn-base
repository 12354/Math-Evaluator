using System.Collections.Generic;

namespace Qfi.AST
{
    class IfStatement : Statement
    {
        Statement Condition;
        StatementList Then;
        StatementList Else;
        public IfStatement(Statement cond, StatementList then, StatementList _else)
        {
            Condition = cond;
            Then = then;
            Else = _else;
        }
        public override string ToString()
        {
            string form = string.Format("if({0}) then {1}", Condition, Then);
            if(Else != null)
                form += "else " + Else.ToString();
            return form;
        }
        public override float Calculate(Dictionary<string, float> variables, Dictionary<string, Function> functions)
        {
            if (Condition.Calculate(variables, functions) != 0)
                Then.Calculate(variables, functions);
            else if (Else != null)
                Else.Calculate(variables, functions);
            return 0;
        }
        public override void Compile(System.Reflection.Emit.ILGenerator emit)
        {
            throw new System.NotImplementedException();
        }
    }
}
