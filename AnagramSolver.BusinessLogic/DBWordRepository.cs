using AnagramSolver.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AnagramSolver.BusinessLogic
{
    public class DBWordRepository : IWordRepository
    {
        private DBConnectionConfig _dbConConf;

        private SqlConnection cn = new();

        public DBWordRepository(IOptions<DBConnectionConfig> dbConConf)
        {
            _dbConConf = dbConConf.Value;

            cn.ConnectionString = _dbConConf.DBConnection;
        }
        public IList<WordModel> GetWords()
        {
            IList<WordModel> words = new List<WordModel>();

            try
            {
                OpenConnection();

                SqlCommand cmd = new();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Word ORDER BY Word";
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        words.Add(new WordModel(Convert.ToInt32(dr["Id"]),
                            Convert.ToString(dr["Word"]),
                            Convert.ToInt32(dr["Category"])));
                    }
                }
                dr.Close();
                
                return words;
            }
            finally
            {
                CloseConnection();
            }
        }

        public IList<WordModel> SearchWords(string myWord)
        {
            List<WordModel> words = new List<WordModel>();
            try
            {
                OpenConnection();

                SqlCommand cmd = new();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM Word WHERE Word LIKE '%' + @myWord + '%' ORDER BY Word";

                SqlParameter param = new SqlParameter("@myWord", myWord);
                cmd.Parameters.Add(param);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        words.Add(new WordModel(Convert.ToInt32(dr["Id"]),
                            Convert.ToString(dr["Word"]),
                            Convert.ToInt32(dr["Category"])));
                    }
                }
                dr.Close();                

                return words;
            }
            finally
            {
                CloseConnection();
            }
        }
        
        public void InsertCachedWord(IList<WordModel> words, string myWord)
        {
            try
            {
                OpenConnection();

                SqlCommand cmd;

                foreach (WordModel ana in words)
                {
                    cmd = new();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"INSERT INTO CachedWord (SearchingWord, AnagramId) " +
                        $"VALUES " +
                            $"(@myWord, @AnagramId)";

                    SqlParameter paramWord = new SqlParameter("@myWord", myWord);
                    SqlParameter paramAnagram = new SqlParameter("@AnagramId", ana.Id);
                    cmd.Parameters.Add(paramWord);
                    cmd.Parameters.Add(paramAnagram);
                    cmd.ExecuteNonQuery();
                }                
            }
            finally
            {
                CloseConnection();
            }          
        }

        public bool CheckCachedWord(string myWord)
        {
            try
            {

                OpenConnection();

                SqlCommand cmd = new();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT COUNT(SearchingWord) AS CountCachedWord FROM CachedWord WHERE SearchingWord = @myWord";

                SqlParameter param = new SqlParameter("@myWord", myWord);
                cmd.Parameters.Add(param);

                SqlDataReader dr = cmd.ExecuteReader();

                bool isWordExist = false;

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        isWordExist = Convert.ToInt32(dr["CountCachedWord"]) > 0 ? true : false;
                    }
                }
                dr.Close();               

                return isWordExist;
            }
            finally
            {
                CloseConnection();
            }
        }

        public IList<WordModel> GetWordFromCache(string myWord)
        {
            List<WordModel> anagrams = new();

            try
            {
                OpenConnection();

                SqlCommand cmd = new();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM Word " +
                    $"INNNER JOIN CachedWord " +
                        $"ON Id = AnagramId AND SearchingWord = @myWord";

                SqlParameter param = new SqlParameter("@myWord", myWord);
                cmd.Parameters.Add(param);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        anagrams.Add(new WordModel(Convert.ToInt32(dr["Id"]), 
                            Convert.ToString(dr["Word"]), 
                            Convert.ToInt32(dr["Category"])));
                    }
                }
                dr.Close();              

                return anagrams;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void DeleteRecordFromWordTable(string myWord)
        {
            try
            {
                OpenConnection();           

                SqlCommand cmd;
                
                cmd = new();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteRecordFromWord";
                cmd.Parameters.Add(new SqlParameter("@target", myWord));                
                cmd.ExecuteNonQuery();                
            }
            finally
            {
                CloseConnection();
            }
        }

        public void InsertUserLogToDB(IList<WordModel> words, string myWord, string userIp)
        {
            try
            {
                OpenConnection();

                SqlCommand cmd;

                foreach (WordModel ana in words)
                {
                    cmd = new();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "InsertUserLog";

                    cmd.Parameters.Add(new SqlParameter("@userIp", userIp));
                    cmd.Parameters.Add(new SqlParameter("@searchingWord", myWord));
                    cmd.Parameters.Add(new SqlParameter("@searchTime", DateTime.Now.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@foundAnagramId", ana.Id));

                    cmd.ExecuteNonQuery();
                }

            }
            finally
            {
                CloseConnection();
            }
        }

        public IList<UserModel> GetUserLogFromDB()
        {
            List<UserModel> users = new();
            try
            {
                OpenConnection();

                SqlCommand cmd = new();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetUserLog";                         

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        users.Add(new UserModel(Convert.ToInt32(dr["UserId"]),Convert.ToString(dr["iP"]),
                            Convert.ToString(dr["SearchingWord"]), Convert.ToDateTime(dr["SearchTime"]), 
                            Convert.ToString(dr["Anagram"])));
                    }
                }
                dr.Close();

                return users;

            }
            finally
            {
                CloseConnection();
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
