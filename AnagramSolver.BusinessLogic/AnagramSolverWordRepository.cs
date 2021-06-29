using System;
using AnagramSolver.Contracts;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolverWordRepository : IWordRepository
    {
        private DBConnectionConfig _dbConConf;
        public AnagramSolverWordRepository(IOptions<DBConnectionConfig> dbConConf)
        {
            _dbConConf = dbConConf.Value;
        }


        public IList<Anagram> GetWords()
        {           
            IList<Anagram> anagrams = new List<Anagram>();

            using (StreamReader sr = File.OpenText("zodynas.txt"))
            {
                string line;              

                while((line = sr.ReadLine()) != null)
                {                      
                    string[] wordPart = line.Split("\t");
                        
                    anagrams.Add(new Anagram(wordPart[0], wordPart[1], wordPart[2], Convert.ToInt32(wordPart[3])));
                }                   
            }

            return anagrams;                        
        }

        public IList<WordModel> GetWordsDB()
        {
            IList<WordModel> words = new List<WordModel>();

            SqlConnection cn = new();
            cn.ConnectionString = _dbConConf.ConnectionString;
            cn.Open();

            SqlCommand cmd = new SqlCommand();
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

            cn.Close();

            return words;
        }
    }
}
