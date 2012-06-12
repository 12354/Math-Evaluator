using System.Collections.Generic;
using System.Reflection.Emit;
namespace Qfi.AST
{
    class NumberStatement : Statement
    {
        float value;
        public NumberStatement(float val)
        {
            value = val;
        }
        public override string ToString()
        {
            return value.ToString();
        }
        public override float Calculate(Dictionary<string,float> variables, Dictionary<string, Function> functions)
        {
            return value;
        }
        public override void Compile(System.Reflection.Emit.ILGenerator emit)
        {
            emit.Emit(OpCodes.Ldc_R4, value);
        }
    }
}
