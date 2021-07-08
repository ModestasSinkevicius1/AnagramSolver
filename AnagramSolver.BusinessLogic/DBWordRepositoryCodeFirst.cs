using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.DAL;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class DBWordRepositoryCodeFirst : IWordRepository
    {

        public IList<WordModel> GetWords()
        {
            IList<WordModel> words = new List<WordModel>();

            using (var db = new AnagramDBCodeFirstContext())
            {
                foreach (var word in db.Word.ToList())
                {
                    words.Add(new WordModel(word.ID, word.Word, word.Category));
                }
            }

            return words;
        }

        public IList<WordModel> SearchWords(string myWord)
        {
            List<WordModel> words = new List<WordModel>();

            using (var db = new AnagramDBCodeFirstContext())
            {
                var wordsFound = from s in db.Word
                                 where s.Word.Contains(myWord)
                                 select s;

                foreach (var word in wordsFound)
                {
                    words.Add(new WordModel(word.ID, word.Word, word.Category));
                }
            }

            return words;
        }

        public void InsertCachedWord(IList<WordModel> words, string myWord)
        {
            foreach (WordModel ana in words)
            {
                var cachedWord = new CachedWord()
                {
                    SearchingWord = myWord,
                    WordID = ana.Id,
                };
                using (var db = new AnagramDBCodeFirstContext())
                {
                    db.Add(cachedWord);
                    db.SaveChanges();
                }
            }
        }

        public bool CheckCachedWord(string myWord)
        {
            bool isWordExist = false;
            int count = 0;

            using (var db = new AnagramDBCodeFirstContext())
            {
                var myWordParam = new SqlParameter("myWord", myWord);

                count = db.CachedWord.Count(t => t.SearchingWord == myWord);
            }

            isWordExist = count > 0 ? true : false;

            return isWordExist;
        }

        public IList<WordModel> GetWordFromCache(string myWord)
        {
            List<WordModel> anagrams = new();

            using (var db = new AnagramDBCodeFirstContext())
            {
                var query = from s in db.CachedWord
                            join sa in db.Word on s.WordID equals sa.ID
                            where s.SearchingWord == myWord
                            select sa;

                foreach (var word in query)
                {
                    anagrams.Add(new WordModel(word.ID, word.Word, word.Category));
                }
            }

            return anagrams;
        }

        public void DeleteRecordFromWordTable(string myWord)
        {
            using (var db = new AnagramDBCodeFirstContext())
            {
                var myWordParam = new SqlParameter("target", myWord);
                db.Database.ExecuteSqlRaw("DeleteRecordFromWord @target", myWordParam);
            }
        }

        public void InsertUserLogToDB(IList<WordModel> words, string myWord, string userIp)
        {
            using (var db = new AnagramDBCodeFirstContext())
            {
                foreach (WordModel ana in words)
                {
                    var userIpParam = new SqlParameter("@userIp", userIp);
                    var myWordParam = new SqlParameter("@searchingWord", myWord);
                    var datetimeParam = new SqlParameter("@searchTime", System.DateTime.Now.ToString());
                    var anagramIdParam = new SqlParameter("@foundAnagramId", ana.Id);

                    db.Database.ExecuteSqlRaw("InsertUserLog @userIp, @searchingWord, " +
                        "@searchTime, @foundAnagramId",
                        userIpParam, myWordParam, datetimeParam, anagramIdParam);
                }
            }
        }

        public IList<UserModel> GetUserLogFromDB()
        {
            List<UserModel> users = new();

            using (var db = new AnagramDBCodeFirstContext())
            {
                var logs = db.UserLog.Include(b => b.Word)
                        .ToList();

                foreach (var log in logs)
                {
                    users.Add(new UserModel(log.UserLogID, log.Ip,
                        log.SearchingWord, log.SearchTime,
                        log.Word.Word));
                }
            }

            return users;
        }
    }
}
