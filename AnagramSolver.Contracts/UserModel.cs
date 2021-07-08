using System;

namespace AnagramSolver.Contracts
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Ip { get; set; }
        public string SearchingWord { get; set; }
        public DateTime SearchTime { get; set; }
        public string FoundAnagram { get; set; }

        public UserModel(int userId, string ip, string searchingWord, 
            DateTime searchTime, string foundAnagram)
        {
            UserId = userId;
            Ip = ip;
            SearchingWord = searchingWord;
            SearchTime = searchTime;
            FoundAnagram = foundAnagram;
        }
    }
}
