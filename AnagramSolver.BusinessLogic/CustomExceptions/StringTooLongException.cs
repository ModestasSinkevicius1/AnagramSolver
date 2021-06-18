using System;

namespace AnagramSolver.BusinessLogic
{
    public class StringTooLongException : Exception
    {
        public StringTooLongException()
        {

        }
        public StringTooLongException(string message)
        : base(message)
        {
        }

        public StringTooLongException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
