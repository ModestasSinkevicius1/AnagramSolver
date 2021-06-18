using System;

namespace AnagramSolver.BusinessLogic
{
    public class WordIsEmptyException : Exception
    {
        public WordIsEmptyException()
        {

        }
        public WordIsEmptyException(string message)
        : base(message)
        {
        }

        public WordIsEmptyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
