using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IWordRepository
    {
        IList<WordModel> GetWords();
        IList<WordModel> SearchWords(string myWord);
    }
}
