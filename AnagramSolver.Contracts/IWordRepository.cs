using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IWordRepository
    {
        IList<WordModel> GetWords();
        IList<WordModel> SearchWords(string myWord);
        void InsertCachedWord(List<WordModel> words, string myWord);
        bool CheckCachedWord(string myWord);
        IList<string> GetWordFromCache(string myWord);
    }
}
