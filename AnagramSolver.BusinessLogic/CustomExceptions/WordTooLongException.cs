using System;

namespace AnagramSolver.BusinessLogic
{
    public class WordTooLongException : Exception
    {
        public WordTooLongException()
        {

        }
        public WordTooLongException(string message)
        : base(message)
        {
        }

        public WordTooLongException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
