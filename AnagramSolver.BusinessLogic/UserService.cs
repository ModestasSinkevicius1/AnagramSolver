using AnagramSolver.Contracts;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic
{
    public class UserService : IUserService
    {
        private IWordRepository _wordRepository;

        public UserService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public void InsertUserLog(IList<WordModel> words, string myWord, string userIp)
        {
            _wordRepository.InsertUserLogToDB(words, myWord, userIp);
        }

        public IList<UserModel> GetUserLog()
        {
            return _wordRepository.GetUserLogFromDB();
        }
    }
}
