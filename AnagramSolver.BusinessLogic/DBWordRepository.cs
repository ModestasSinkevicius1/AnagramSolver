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
        public DBWordRepository(IOptions<DBConnectionConfig> dbConConf)
        {
            _dbConConf = dbConConf.Value;
        }

        public string Key { get; } = "DB";        

        public IList<WordModel> GetWords()
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

        public void Test()
        {

        }
    }
}
