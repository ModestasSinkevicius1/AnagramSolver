using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IUserService
    {
        void InsertUserLog(IList<WordModel> words, string myWord, string userIp);

        IList<UserModel> GetUserLog(); 
    }
}
