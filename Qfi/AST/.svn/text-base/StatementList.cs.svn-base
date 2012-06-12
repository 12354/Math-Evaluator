using System.Collections.Generic;
using System.Text;

namespace Qfi.AST
{
    public class StatementList : List<Statement>
    {
        public void Calculate(Dictionary<string, float> variables, Dictionary<string, Function> functions)
        {
            foreach (Statement statement in this)
            {
                if (variables.ContainsKey("return"))
                    return;
                statement.Calculate(variables, functions);
            }
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (Statement statement in this)
            {
                 str.AppendLine(statement.ToString());
            }
            return str.ToString();
        }
        public void Compile(System.Reflection.Emit.ILGenerator emit)
        {
            foreach (Statement s in this)
                s.Compile(emit);
        }
    }
}
