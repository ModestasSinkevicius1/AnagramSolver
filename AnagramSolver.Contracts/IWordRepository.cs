using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IWordRepository
    {
        IList<Anagram> GetWords();

        IList<WordModel> GetWordsDB();
    }
}
