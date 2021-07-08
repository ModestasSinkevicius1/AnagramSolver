using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class DBWordRepositoryDatabaseFirst : IWordRepository
    {       
        public DBWordRepositoryDatabaseFirst()
        {            
        }

        public IList<WordModel> GetWords()
        {
            IList<WordModel> words = new List<WordModel>();
            
            using (var db = new AnagramDBContext())
            {
                foreach(var word in db.Words.ToList())
                {
                    words.Add(new WordModel(word.Id, word.Word1, word.Category));
                }
            }

            return words.OrderBy(o => o.Word).ToList();
        }

        public IList<WordModel> SearchWords(string myWord)
        {
            List<WordModel> words = new List<WordModel>();
                            
            using (var db = new AnagramDBContext())
            {                
                var wordsFound = from s in db.Words 
                            where s.Word1.Contains(myWord) 
                            select s;

                foreach (var word in wordsFound)
                {
                    words.Add(new WordModel(word.Id, word.Word1, word.Category));
                }
            }         

            return words.OrderBy(o => o.Word).ToList();
        }

        public void InsertCachedWord(IList<WordModel> words, string myWord)
        {                          
            foreach (WordModel ana in words)
            {
                var cachedWord = new CachedWord()
                {                       
                    SearchingWord = myWord,
                    AnagramId = ana.Id,                        
                };
                using(var db = new AnagramDBContext())
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

            using (var db = new AnagramDBContext())
            {
                var myWordParam = new SqlParameter("myWord", myWord);

                count = db.CachedWords.Count(t => t.SearchingWord == myWord);           
            }

            isWordExist = count > 0 ? true : false;

            return isWordExist;            
        }

        public IList<WordModel> GetWordFromCache(string myWord)
        {
            List<WordModel> anagrams = new();
            
            using(var db = new AnagramDBContext())
            {
                var query = from s in db.CachedWords
                            join sa in db.Words on s.AnagramId equals sa.Id
                            where s.SearchingWord == myWord
                            select sa;

                foreach(var word in query)
                {
                    anagrams.Add(new WordModel(word.Id, word.Word1, word.Category));
                }
            }              

            return anagrams;          
        }

        public void DeleteRecordFromWordTable(string myWord)
        {        
            using(var db = new AnagramDBContext())
            {
                var myWordParam = new SqlParameter("target", myWord);
                db.Database.ExecuteSqlRaw("DeleteRecordFromWord @target", myWordParam);
            }            
        }

        public void InsertUserLogToDB(IList<WordModel> words, string myWord, string userIp)
        {            
            using (var db = new AnagramDBContext())
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
    
            using(var db = new AnagramDBContext())
            {
                var logs = db.UserLogs.Include(b => b.FoundAnagram).ToList();

                foreach (var log in logs)
                {              
                    users.Add(new UserModel(log.UserId, log.Ip, 
                        log.SearchingWord, log.SearchTime, 
                        log.FoundAnagram.Word1));
                }
            }
                
            return users;           
        }        
    }
}
