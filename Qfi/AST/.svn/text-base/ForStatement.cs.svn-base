using System.Collections.Generic;

namespace Qfi.AST
{
    class ForStatement : Statement
    {
        Statement Initialize;
        Statement Condition;
        Statement Step;
        StatementList Body;
        public ForStatement(Statement Initialize, Statement Condition, Statement Step, StatementList Body)
        {
            this.Initialize = Initialize;
            this.Condition = Condition;
            this.Body = Body;
            this.Step = Step;
        }
        public ForStatement(StatementList Conditions, StatementList Body)
        {
            this.Initialize = Conditions[0];
            this.Condition = Conditions[1];
            this.Step = Conditions[2];
            this.Body = Body;
        }
          public override float Calculate(Dictionary<string, float> variables, Dictionary<string, Function> functions)
          {
              Initialize.Calculate(variables, functions);
              while (Condition.Calculate(variables, functions) > 0)
              {
                  Body.Calculate(variables, functions);
                  Step.Calculate(variables, functions);
              }
              return 0;
          }
          public override void Compile(System.Reflection.Emit.ILGenerator emit)
          {
              throw new System.NotImplementedException();
          }
    }
}
