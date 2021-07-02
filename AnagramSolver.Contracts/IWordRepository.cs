using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IWordRepository
    {
        IList<WordModel> GetWords();

        IList<WordModel> SearchWords(string myWord);

        void InsertCachedWord(IList<WordModel> words, string myWord);

        bool CheckCachedWord(string myWord);

        IList<WordModel> GetWordFromCache(string myWord);

        void DeleteRecordFromWordTable(string myWord);

        void InsertUserLogToDB(IList<WordModel> words, string MyWord, string userIp);

        IList<UserModel> GetUserLogFromDB();
    }
}
