using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IWordService
    {
        List<string> GetWords(int pageNumber, int pageSize, string myWord);

        List<WordModel> GetAnagramsByQuery(string myWords);
    }
}
