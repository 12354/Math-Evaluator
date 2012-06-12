using System.Collections.Generic;
namespace Qfi
{
    public class Lexer
    {
        string _src;
        int _pos;
        public Lexer()
        {
        }
        public List<Token> Scan(string source)
        {
            source += " ";
            _src = source;
            _pos = 0;
            Token token = null;
            List<Token> tokenlist = new List<Token>();
            do
            {
                tokenlist.Add(token = GetToken());
            } while (token.Type != TokenType.EOF);
            return tokenlist;
        }
        char LastChar = ' ';
        private Token GetToken()
        {
            //EOF
            if (EOF())
            {
                return new Token("",TokenType.EOF);
            }

            while (char.IsWhiteSpace(LastChar))
            {
                LastChar = GetChar();
                if (EOF())
                    return new Token("",TokenType.EOF);
            }

            //String
            if (char.IsLetter(LastChar))
            {
                string Identifier = LastChar.ToString();
                while (char.IsLetterOrDigit((LastChar = GetChar())))
                    Identifier += LastChar;
                return new Token(Identifier,TokenType.Identifier);
            }
            //Number
            if (char.IsDigit(LastChar))
            {
                string Number = "";
                bool comma = false;
                do
                {
                    if (LastChar == '.')
                        comma = true;
                    Number += LastChar;
                    LastChar = GetChar();
                } while (char.IsDigit(LastChar) || (LastChar == '.' && !comma));
                return new Token(Number, TokenType.Number);
            }
            //Comment
            if (LastChar == '#')
            {
                do
                {
                    LastChar = GetChar();
                } while (!EOF() && LastChar != '\n' && LastChar != '\r');
                if (!EOF())
                    return GetToken();
            }
            string ThisChar = LastChar.ToString();
            while (!char.IsLetterOrDigit(LastChar = GetChar()) && !char.IsWhiteSpace(LastChar) && !IsSpecialCharacter(LastChar) && !IsSpecialCharacter(ThisChar))
                ThisChar += LastChar;

            return new Token(ThisChar, TokenType.Character);
        }
        private bool IsSpecialCharacter(char c)
        {
            return c == ';' || c == '(' || c == ')' || c == '{' || c == '}';
        }
        private bool IsSpecialCharacter(string c)
        {
            return IsSpecialCharacter(c[c.Length-1]);
        }
        private bool EOF()
        {
            return _pos == _src.Length;
        }
        private char GetChar()
        {
            return _src[_pos++];
        }
    }
}
