using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IUserLogService
    {
        void InsertUserLog(IList<WordModel> words, string myWord, string userIp);

        IList<UserModel> GetUserLog(); 
    }
}
