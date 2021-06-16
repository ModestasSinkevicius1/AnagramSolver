using System;
using AnagramSolver.Contracts;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolverWordRepository : IWordRepository
    {       
        public IList<Anagram> GetWords()
        {           
            IList<Anagram> la = new List<Anagram>();

            using (StreamReader sr = File.OpenText("zodynas.txt"))
            {
                string line;                    

                while((line = sr.ReadLine()) != null)
                {                      
                    string[] word = line.Split("\t");
                        
                    la.Add(new Anagram(word[0], word[1], word[2], Convert.ToInt32(word[3])));
                }                   
            }
            return la;                        
        }
    }
}
