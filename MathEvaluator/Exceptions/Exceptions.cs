using System;

namespace MathEvaluator
{
    namespace Exceptions
    {
        public class MismatchedParenthesisException : Exception
        {
            public MismatchedParenthesisException(string message) : base(message) { }
        }
        public class UndefinedResultException : Exception
        {
            public UndefinedResultException(string message) : base(message) { }
        }
        public class UndefinedFunctionException : Exception
        {
            public UndefinedFunctionException(string message) : base(message) { }
        }
        public class UndefinedVariableException : Exception
        {
            public UndefinedVariableException(string message) : base(message) { }
        }
        public class FunctionAlreadyDefinedException : Exception
        {
            public FunctionAlreadyDefinedException(string message) : base(message) { }
        }
        public class NotEnoughArgumentsException : Exception
        {
            public NotEnoughArgumentsException(string message) : base(message) { }
        }
        public class InvalidOperatorIdentifierException : Exception
        {
            public InvalidOperatorIdentifierException(string message) : base(message) { }
        }
        public class InvalidFunctionIdentifierException : Exception
        {
            public InvalidFunctionIdentifierException(string message) : base(message) { }
        }
        public class InvalidFunctionArgumentCountException : Exception
        {
            public InvalidFunctionArgumentCountException(string message) : base(message) { }
        }
    }
}
