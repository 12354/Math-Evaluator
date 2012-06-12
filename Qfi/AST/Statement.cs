using System.Collections.Generic;
using System.Reflection.Emit;
namespace Qfi.AST
{
    public abstract class Statement
    {
        public abstract float Calculate(Dictionary<string, float> variables, Dictionary<string, Function> functions);
        public abstract void Compile(ILGenerator emit);
    }
}
