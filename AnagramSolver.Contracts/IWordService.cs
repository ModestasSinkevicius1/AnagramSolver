using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IWordService
    {
        List<string> GetWords(int pageNumber, int pageSize);
    }
}
