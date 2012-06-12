using System;
using System.Collections.Generic;
using Qfi.AST;
namespace Qfi
{
    public class Parser
    {
        Token CurToken;
        List<Token> Tokens;
        int Position;
        Dictionary<string, int> BinaryOperatorPrecedence;
        public Parser()
        {
            BinaryOperatorPrecedence = new Dictionary<string, int>()
            {
                {":" , 1 },
                {"=" , 2 } ,  
                {"+=", 2 } ,
                {"-=", 2 } ,
                {"*=", 2 } ,
                {"/=", 2 } ,
                {"<" , 10},
                {">" , 10},
                {"==" , 10},
                {"+" , 20},
                {"-" , 20},
                {"*" , 40},
                {"/" , 40},
            };
        }
        public Token GetNextToken()
        {
            return CurToken = Tokens[Position++];
        }
        public GeneratedCode Parse(List<Token> _tokens)
        {
            Dictionary<string, Function> Functions = new Dictionary<string, Function>();
            StatementList TopLevelStatements = new StatementList();
            Tokens = _tokens;
            Position = 0;

            GetNextToken(); //Initialize first token
            while (true)
            {
                if (CurToken.Type == TokenType.EOF)
                    break;
                if (CurToken.IsCharacter(";"))
                    GetNextToken(); //eat ;
                else if (CurToken.IsIdentifier("function"))
                {
                    Function func = ParseFunction();
                    Functions[func.Name] = func;
                }
                else
                    TopLevelStatements.Add(ParseStatement());
            }
            Function TopLevel = new Function(TopLevelStatements);
            return new GeneratedCode()
            {
                TopLevelStatements = TopLevel,
                Functions = Functions
            };
        }

        private Function ParseFunction()
        {
            GetNextToken(); //eat function
            FunctionPrototype prototype = ParseFunctionPrototype();
            if (!CurToken.IsCharacter("{"))
                throw new Exception("Expected function body start {");

            StatementList body = ParseStatementList(false);
            return new Function(prototype, body);
        }

        private Statement ParseStatement()
        {
            Statement Left = ParsePrimary();
            if (Left == null)
                throw new Exception("Invalid expression");
            return ParseOperatorRHS(0, Left);
        }
        private StatementList ParseStatementList(bool AllowSingleStatement)
        {
            StatementList body = new StatementList();
            if (CurToken.IsCharacter("{") || !AllowSingleStatement)
            {
                GetNextToken(); //eat {
                while (!CurToken.IsCharacter("}"))
                {
                    body.Add(ParseStatement());
                }
                GetNextToken(); //eat }
            }
            else
                body.Add(ParseStatement());
            return body;
        }

        private Statement ParseOperatorRHS(int precedence, Statement Left)
        {
            while (true)
            {
                int LeftPrec = GetTokenPrecedence();
                if (LeftPrec < precedence)
                    return Left;
                string Operator = CurToken.Value; ;
                GetNextToken(); //eat left

                Statement Right = ParsePrimary();
                int RightPrec = GetTokenPrecedence();
                if (LeftPrec < RightPrec)
                {
                    Right = ParseOperatorRHS(LeftPrec + 1, Right);
                }
                Left = new BinaryOperator(Operator, Left, Right);
            }
        }

        private int GetTokenPrecedence()
        {
            int prec;
            if (BinaryOperatorPrecedence.TryGetValue(CurToken.Value, out prec) && prec > 0)
                return prec;
            return -1;
        }

        private Statement ParsePrimary()
        {
            if (CurToken.Type == TokenType.Identifier)
            {
                switch (CurToken.Value.ToLower())
                {
                    case "if":
                        return ParseIfStatement();
                    case "do":
                        return ParseDoStatement();
                    case "for":
                        return ParseForStatement();
                    case "while":
                        return ParseWhileStatement();
                    case "return":
                        return ParseReturnStatement();
                    default:
                        return ParseIdentifierStatement();
                }
            }
            if (CurToken.Type == TokenType.Number)
                return ParseNumberStatement();
            if (CurToken.IsCharacter(";"))
            {
                GetNextToken();
                return ParsePrimary();
            }
            if (CurToken.IsCharacter("("))
                return ParseParenthesisStatement();
            throw new Exception("Invalid Expression");
        }

        private Statement ParseDoStatement()
        {
            GetNextToken(); //eat do;
            StatementList body = ParseStatementList(true);
            if(!CurToken.IsIdentifier("while"))
                throw new Exception("Expected while after do");
            GetNextToken(); //eat while
            Statement condition = ParseCondition();
            return new DoStatement(condition, body);
        }

        private Statement ParseReturnStatement()
        {
            GetNextToken(); //eat return
            return new ReturnStatement(ParseStatement());
        }

        private Statement ParseParenthesisStatement()
        {
            GetNextToken(); //eat (
            Statement inner = ParseStatement();
            if (!CurToken.IsCharacter(")"))
                throw new Exception("Expected )");
            GetNextToken(); //eat )
            return inner;
        }

        private Statement ParseNumberStatement()
        {
            float value = float.Parse(CurToken.Value);
            Statement result = new NumberStatement(value);
            GetNextToken();
            return result;
        }

        private Statement ParseIdentifierStatement()
        {
            string identName = CurToken.Value;
            GetNextToken(); //eat identifier
            if (!CurToken.IsCharacter("("))
                return new VariableStatement(identName);

            GetNextToken(); //eat (
            StatementList arguments = new StatementList();
            if (!CurToken.IsCharacter(")"))
            {
                while (true)
                {
                    arguments.Add(ParseStatement());
                    if (CurToken.IsCharacter(")"))
                        break;
                    if (!CurToken.IsCharacter(","))
                        throw new Exception("\",\" expected");
                    GetNextToken(); //eat argument
                }
            }
            GetNextToken(); //eat }

            return new CallStatement(identName, arguments);
        }
        private Statement ParseCondition()
        {
            if (!CurToken.IsCharacter("("))
                throw new Exception("Expected condition");
            GetNextToken(); //eat (
            Statement Condition = ParseStatement();
            if (!CurToken.IsCharacter(")"))
                throw new Exception("Expected end of condition");
            GetNextToken(); //eat )
            return Condition;
        }
        private StatementList ParseConditions(int n)
        {
            StatementList statements = new StatementList();
            if (!CurToken.IsCharacter("("))
                throw new Exception("Expected condition after if");
            GetNextToken(); //eat (
            for (int i = 1; i <= n; i++)
            {
                statements.Add(ParseStatement());
                if (i == n)
                    break;
                if (!CurToken.IsCharacter(";"))
                    throw new Exception("Expected ;");
                GetNextToken(); //eat ;
            }

            if (!CurToken.IsCharacter(")"))
                throw new Exception("Expected end of condition");
            GetNextToken(); //eat )
            return statements;

        }
        private Statement ParseWhileStatement()
        {
            GetNextToken(); //eat while
            Statement Condition = ParseCondition();
            StatementList body = ParseStatementList(true);
            return new WhileStatement(Condition, body);
        }

        private Statement ParseForStatement()
        {
            GetNextToken(); //eat for
            StatementList conditions = ParseConditions(3);
            StatementList body = ParseStatementList(true);
            return new ForStatement(conditions, body);
        }

        private Statement ParseIfStatement()
        {
            GetNextToken(); //eat if
            Statement Condition = ParseCondition();

            StatementList Then = ParseStatementList(true);
            StatementList Else = null;
            if (CurToken.IsIdentifier("else"))
            {
                GetNextToken(); //eat else
                Else = ParseStatementList(true);
            }
            return new IfStatement(Condition, Then, Else);
        }

        private FunctionPrototype ParseFunctionPrototype()
        {
            if (CurToken.Type != TokenType.Identifier)
                throw new Exception("Expected function identifier");
            string FunctionName = CurToken.Value;
            GetNextToken(); //eat function identifier

            if (!CurToken.IsCharacter("("))
                throw new Exception("Expected ( after function identifier");
            List<string> Arguments = new List<string>();
            while (true)
            {
                if (GetNextToken().Type == TokenType.Identifier) //Argument identifier
                {
                    Arguments.Add(CurToken.Value);
                    if (GetNextToken().IsCharacter(","))
                        continue;
                }
                break;
            }
            if (!CurToken.IsCharacter(")"))
                throw new Exception("Expected ) after function arguments");
            GetNextToken(); //eat )
            return new FunctionPrototype(FunctionName, Arguments);
        }
    }
}
