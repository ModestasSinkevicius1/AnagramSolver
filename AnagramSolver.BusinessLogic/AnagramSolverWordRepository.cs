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
    }
}
