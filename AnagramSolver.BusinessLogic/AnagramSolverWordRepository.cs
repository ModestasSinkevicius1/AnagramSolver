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
        public IList<WordModel> GetWords()
        {           
            IList<WordModel> anagrams = new List<WordModel>();

            using (StreamReader sr = File.OpenText("zodynas.txt"))
            {
                string line;

                int row = 0;

                while((line = sr.ReadLine()) != null)
                {                      
                    string[] wordPart = line.Split("\t");
                        
                    anagrams.Add(new WordModel(row++, wordPart[0], Convert.ToInt32(wordPart[3])));
                }                   
            }

            return anagrams;                        
        }
        
        public IList<WordModel> SearchWords(string myWord)
        {
            return null;
        }        

        public void InsertCachedWord(List<WordModel> words, string myWord)
        {
            
        }
        public bool CheckCachedWord(string myWord)
        {
            return false;
        }

        public IList<string> GetWordFromCache(string myWord)
        {
            return null;
        }
    }
}
