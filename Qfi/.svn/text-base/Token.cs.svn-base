using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qfi
{
    public enum TokenType
    {
        EOF,
        Character,
        Identifier,
        Number
    }
    public class Token
    {
 
        public Token(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }
        public bool IsIdentifier(string identifier)
        {
            return Type == TokenType.Identifier && Value.ToLower() == identifier.ToLower();
        }
        public bool IsCharacter(string identifier)
        {
            return Type == TokenType.Character && Value.ToLower() == identifier.ToLower();
        }

        public string Value { get;private set; }
        public TokenType Type { get; private set; }
        public override string ToString()
        {
            return Value;
        }
    }
}
