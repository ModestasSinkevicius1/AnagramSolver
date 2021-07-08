using AnagramSolver.Contracts;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic
{
    public class UserLogService : IUserLogService
    {
        private readonly IWordRepository _wordRepository;

        public UserLogService(IWordRepository wordRepository)
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
