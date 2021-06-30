using AnagramSolver.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class DBWordService : IWordService
    {
        private readonly IWordRepository _wordRepository;
        private DBConnectionConfig _dbConConf;

        public DBWordService(IWordRepository wordRepository, IOptions<DBConnectionConfig> dbConConf)
        {
            _wordRepository = wordRepository;
            _dbConConf = dbConConf.Value;
        }
        public List<string> GetWords(int pageNumber, int pageSize, string myWord)
        {
            List<string> words = new List<string>();

            if (pageNumber < 0)
                pageNumber = 0;

            if (pageNumber * pageSize <= 0 && pageSize <= 0)
            {
                pageNumber = 0;
                pageSize = 100;
            }
            if (string.IsNullOrWhiteSpace(myWord) || myWord == "*")
            {
                words = _wordRepository.GetWords().Select(o => o.Word)
                .Skip(pageSize * pageNumber)
                .Take(pageSize).ToList();

                return words;
            }          

            SqlConnection cn = new();
            cn.ConnectionString = _dbConConf.ConnectionString;
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT DISTINCT Word FROM Word WHERE Word LIKE '%' + @myWord + '%'";

            SqlParameter param = new SqlParameter("@myWord", myWord);
            cmd.Parameters.Add(param);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    words.Add(Convert.ToString(dr["Word"]));
                }
            }
            dr.Close();

            cn.Close();

            return words.Skip(pageSize * pageNumber)
                .Take(pageSize).ToList();
        }
    }
}
