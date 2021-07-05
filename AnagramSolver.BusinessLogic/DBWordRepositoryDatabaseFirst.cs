using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class DBWordRepositoryDatabaseFirst : IWordRepository
    {
        private DBConnectionConfig _dbConConf;

        private SqlConnection cn = new();

        public DBWordRepositoryDatabaseFirst(IOptions<DBConnectionConfig> dbConConf)
        {
            _dbConConf = dbConConf.Value;

            cn.ConnectionString = _dbConConf.DBConnection;
        }
        public IList<WordModel> GetWords()
        {
            IList<WordModel> words = new List<WordModel>();

            try
            {
                using (var db = new AnagramDBContext())
                {
                    foreach(var word in db.Words.ToList())
                    {
                        words.Add(new WordModel(word.Id, word.Word1, word.Category));
                    }
                }

                return words;
            }
            finally
            {

            }
        }

        public IList<WordModel> SearchWords(string myWord)
        {
            List<WordModel> words = new List<WordModel>();
            try
            {                
                using (var db = new AnagramDBContext())
                {
                    var myWordParam = new SqlParameter("myWord", myWord);

                    var blogs = db.Words
                        .FromSqlRaw("SELECT * FROM Word WHERE Word LIKE '%' + @myWord + '%'", myWordParam).ToList();

                    foreach (var word in blogs)
                    {
                        words.Add(new WordModel(word.Id, word.Word1, word.Category));
                    }
                }         

                return words;
            }
            finally
            {
                
            }
        }

        public void InsertCachedWord(IList<WordModel> words, string myWord)
        {
            try
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
            finally
            {
                
            }
        }

        public bool CheckCachedWord(string myWord)
        {
            try
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
            finally
            {
                
            }
        }

        public IList<WordModel> GetWordFromCache(string myWord)
        {
            List<WordModel> anagrams = new();

            try
            {
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
            finally
            {
                
            }
        }

        public void DeleteRecordFromWordTable(string myWord)
        {
            try
            {
                using(var db = new AnagramDBContext())
                {
                    var myWordParam = new SqlParameter("target", myWord);
                    db.Database.ExecuteSqlRaw("DeleteRecordFromWord @target", myWordParam);
                }
            }
            finally
            {
               
            }
        }

        public void InsertUserLogToDB(IList<WordModel> words, string myWord, string userIp)
        {
            try
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
            finally
            {
                
            }
        }

        public IList<UserModel> GetUserLogFromDB()
        {
            List<UserModel> users = new();

            IQueryable<UserLog> logs;

            try
            {
                using(var db = new AnagramDBContext())
                {
                    logs = db.UserLogs
                    .FromSqlRaw("SELECT UserId, Ip, SearchingWord, " +
                    "SearchTime, FoundAnagramId FROM UserLog");

                    foreach (var log in logs)
                    {              
                        users.Add(new UserModel(log.UserId, log.Ip, log.SearchingWord,
                            log.SearchTime, log.FoundAnagramId.ToString()));
                    }
                }
                
                return users;

            }
            finally
            {
                
            }
        }

        private void OpenConnection()
        {
            cn.Open();
        }

        private void CloseConnection()
        {
            cn.Close();
        }
    }
}
