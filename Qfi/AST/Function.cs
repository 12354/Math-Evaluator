using System.Collections.Generic;

namespace Qfi.AST
{
    public class Function
    {

        public FunctionPrototype Prototype { get; private set; }
        public StatementList Statements { get; private set; }
        public string Name { get { return Prototype.Name; } }
        public Function(FunctionPrototype proto, StatementList statements)
        {
            Statements = statements;
            Prototype = proto;
        }
        public Function(StatementList statements)
        {
            Prototype = new FunctionPrototype("", new string[0]);
            Statements = statements;
        }
        public virtual float Calculate(Dictionary<string, float> variables, Dictionary<string, Function> functions)
        {
            float result;
            foreach (Statement statement in Statements)
            {
                statement.Calculate(variables, functions);
                if (variables.TryGetValue("return", out result))
                    return result;
            }
            return 0;
        }
    }
}