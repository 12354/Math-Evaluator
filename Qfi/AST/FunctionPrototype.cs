using System.Collections.Generic;
using System.Linq;

namespace Qfi.AST
{
    public class FunctionPrototype
    {
        public string Name { get; private set; }
        public string[] Arguments { get; private set; }
        public FunctionPrototype(string name, IEnumerable<string> args)
        {
            Arguments = args.ToArray();
            Name = name;
        }
    }
}
