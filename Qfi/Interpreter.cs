using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qfi.AST;
namespace Qfi
{
    public class Interpreter
    {
        Dictionary<string, Function> Functions;
        Dictionary<string, float> Variables;
        public Interpreter()
        {
            Functions = new Dictionary<string, Function>();
            Variables = new Dictionary<string, float>();
            Functions["print"] = new ExternFunction(new FunctionPrototype("print", new List<string>() { "x" }), x => { Console.WriteLine(x); return 0; });
            Functions["read"] = new ExternFunction(new FunctionPrototype("read", new List<string>()), () =>{return Convert.ToSingle(Console.ReadLine()); });
            Functions["putchar"] = new ExternFunction(new FunctionPrototype("putchar", new List<string>() { "x" }), x => { Console.Write((char)x); return 0; });
            Functions["putpos"] = new ExternFunction(new FunctionPrototype("putpos", new List<string>() { "x", "y" }), (x, y) => { Console.SetCursorPosition((int)x, (int)y); return 0; });
            //add new put_pos function
            Functions["put_pos"] = new ExternFunction(new FunctionPrototype("putpos", new List<string>() { "x", "y" }), (x, y) => { Console.SetCursorPosition((int)x, (int)y); return 0; });
        }
        public void SetVariable(string s, float value)
        {
            Variables[s] = value;
        }
        public float GetVariable(string s)
        {
            if(Variables.ContainsKey(s))
                return Variables[s];
            return 0;
        }
        public void AddFunction(string name, ExternFunction.CalculateDelegate2 func2)
        {
            Functions[name] = new ExternFunction(new FunctionPrototype(name, new List<string>() { "x", "y" }), func2);
        }
        public void AddFunction(string name, ExternFunction.CalculateDelegate1 func2)
        {
            Functions[name] = new ExternFunction(new FunctionPrototype(name, new List<string>() { "x", "y" }), func2);
        }
        public void AddFunction(string name, ExternFunction.CalculateDelegate func2)
        {
            Functions[name] = new ExternFunction(new FunctionPrototype(name, new List<string>()), func2);
        }
        public void AddFunction(string name, ExternFunction.CalculateDelegate3 func2)
        {
            Functions[name] = new ExternFunction(new FunctionPrototype(name, new List<string>() { "x", "y", "z" }), func2);
        }
        public float Interpret(GeneratedCode code)
        {
            if (code == null)
                return 0;
            foreach (var v in Functions)
            {
                if (!code.Functions.ContainsKey(v.Key))
                    code.Functions[v.Key] = v.Value;
            }
            float returns = 0;
            foreach (Statement statement in code.TopLevelStatements.Statements)
            {
                returns = statement.Calculate(Variables, code.Functions);
            }
            return returns;
        }
    }
}
