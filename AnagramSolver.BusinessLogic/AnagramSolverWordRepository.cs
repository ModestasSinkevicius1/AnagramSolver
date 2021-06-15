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
        private List<Anagram> la = new List<Anagram>();

        public IList<Anagram> GetWords()
        {
            try
            {
                using (StreamReader sr = File.OpenText("zodynas.txt"))
                {
                    string line;

                    //int row = 0;

                    //string[] word = new string[4];

                    //string fullWord = "";

                    while((line = sr.ReadLine()) != null)
                    {
                        
                        string[] word = Regex.Split(line, @" +");
                        
                        Console.WriteLine(word[0]);
                        
                        //la.Add(new Anagram(word[0], word[1], word[2], Convert.ToInt32(word[3])));
                    }                   
                }
                return la;
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadLine();
                return null;
            }
        }
    }
}
