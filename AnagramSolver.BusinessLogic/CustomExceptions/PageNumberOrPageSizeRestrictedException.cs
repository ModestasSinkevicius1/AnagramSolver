using System;

namespace AnagramSolver.BusinessLogic
{
    public class PageNumberOrPageSizeRestrictedException : Exception
    {
        public PageNumberOrPageSizeRestrictedException()
        {

        }
        public PageNumberOrPageSizeRestrictedException(string message)
        : base(message)
        {
        }

        public PageNumberOrPageSizeRestrictedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
