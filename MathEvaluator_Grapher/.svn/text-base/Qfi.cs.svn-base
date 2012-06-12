using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathEvaluator.MathOperations.Functions;
using Qfi;
namespace MathEvaluator_Grapher
{
    class QfiWrapper : Function
    {
        public static GeneratedCode code;
        public static Interpreter interpreter = new Interpreter();
        public static Parser parser = new Parser();
        public static Lexer lexer = new Lexer();
        public static int arg_count = 2;
        public override int ArgumentCount
        {
            get
            {
                return arg_count;
            }
            set
            {
                base.ArgumentCount = value;
            }
        }
        public static void Compile(string source)
        {
            //source = source.Replace("\r", "");
            try
            {
                List<Token> t = lexer.Scan(source);
                code = parser.Parse(t);
            }
            catch (Exception)
            {
            }
        }
        public static void AddFunctions(IEnumerable<MathEvaluator.MathOperations.MathOperation> funcs)
        {
            foreach (var func in funcs)
            {
                Function f = func as Function;
                if (f != null)
                {
                    interpreter.AddFunction(f.ToString(), x => f.Calculate((float)x));
                }
            }
        }
        public override MathEvaluator.MathOperations.Number Calculate(float a)
        {
            if (interpreter != null)
            {
                interpreter.SetVariable("x", a);
                interpreter.Interpret(code);
                return interpreter.GetVariable("result");
            }
            return 0;
        }
        public override MathEvaluator.MathOperations.Number Calculate(float a,float b)
        {
            if (interpreter != null)
            {
                try
                {
                    interpreter.SetVariable("x", a);
                    interpreter.SetVariable("y", b);
                    interpreter.Interpret(code);
                    return interpreter.GetVariable("result");
                }
                catch (Exception ex)
                {
                    
                    throw;
                }

     
            }
            return 0;
        }
        public override MathEvaluator.MathOperations.Number Calculate(float a, float b,float c)
        {
            if (interpreter != null)
            {
                interpreter.SetVariable("x", a);
                interpreter.SetVariable("y", b);
                interpreter.SetVariable("z", c);
                interpreter.Interpret(code);
                return interpreter.GetVariable("result");
            }
            return 0;
        }
        public override string ToString()
        {
            return "qfi";
        }
    }
}
