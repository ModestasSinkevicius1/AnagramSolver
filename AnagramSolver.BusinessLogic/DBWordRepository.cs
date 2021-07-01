using AnagramSolver.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.BusinessLogic
{
    public class DBWordRepository : IWordRepository
    {
        private DBConnectionConfig _dbConConf;

        private SqlConnection cn = new();

        public DBWordRepository(IOptions<DBConnectionConfig> dbConConf)
        {
            _dbConConf = dbConConf.Value;
        }
        public IList<WordModel> GetWords()
        {
            IList<WordModel> words = new List<WordModel>();

            OpenConnection();         

            SqlCommand cmd = new();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Word";
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

            CloseConnection();

            return words;
        }

        public IList<WordModel> SearchWords(string myWord)
        {
            List<WordModel> words = new List<WordModel>();

            OpenConnection();

            SqlCommand cmd = new();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM Word WHERE Word LIKE '%' + @myWord + '%'";

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

            CloseConnection();

            return words;
        }

        private void OpenConnection()
        {
            cn.ConnectionString = _dbConConf.ConnectionString;
            cn.Open();
        }

        private void CloseConnection()
        {
            cn.Close();
        }

        public void InsertCachedWord(List<string> words, string myWord)
        {
            OpenConnection();

            SqlCommand cmd;

            foreach (string ana in words)
            {
                cmd = new();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"INSERT INTO CachedWord (SearchingWord, AnagramId) " +
                    $"SELECT " +
                        $"@myWord, Id from Word where Word = @Anagram";

                SqlParameter paramWord = new SqlParameter("@myWord", myWord);
                SqlParameter paramAnagram = new SqlParameter("@Anagram", ana);
                cmd.Parameters.Add(paramWord);
                cmd.Parameters.Add(paramAnagram);
                cmd.ExecuteNonQuery();
            }

            CloseConnection();
        }

        public bool CheckCachedWord(string myWord)
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

            CloseConnection();

            return isWordExist;
        }

        public IList<string> GetWordFromCache(string myWord)
        {
            List<string> anagrams = new();

            OpenConnection();

            SqlCommand cmd = new();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT Word FROM Word " +
                $"INNNER JOIN CachedWord " +
                    $"ON Id = AnagramId AND SearchingWord = @myWord";

            SqlParameter param = new SqlParameter("@myWord", myWord);
            cmd.Parameters.Add(param);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    anagrams.Add(Convert.ToString(dr["Word"]));
                }
            }
            dr.Close();

            CloseConnection();

            return anagrams;
        }
    }
}
