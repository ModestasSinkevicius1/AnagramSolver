using System;
using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolverLogic : IAnagramSolver
    {
        private IWordRepository iwr;
        private IList<string> aList = new List<string>();
        
        public AnagramSolverLogic(IWordRepository iwr)
        {
            this.iwr = iwr;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            iwr.GetWords();

            string wordPattern = @"^[labas]{5}$";
                //"^" + "[" + myWords + "]" + "{" + myWords.Length + "}" + "$";

            Regex filterWord = new Regex(wordPattern);

            if (isLetterNotMoreThanGiven("labas", "balas"));

            //Checking if given word matches set of characters                       
            foreach (Anagram ana in iwr.GetWords())
            {
                if (filterWord.IsMatch(ana.Word))
                {
                    if(!aList.Contains(ana.Word) && ana.Word != myWords)
                        //if(isLetterNotMoreThanGiven(myWords, ana.Word))
                            aList.Add(ana.Word);
                }
            }           

            return aList;
        }

        public bool isLetterNotMoreThanGiven(string refWord, string checkWord)
        {
            int countRefLetter = 0;
            int countCheckLetter = 0;

            List<char> blacklistChar = new List<char>();

            List<char> blacklistCharCheck = new List<char>();

            foreach(char targetLetter in refWord)
            {                
                if (!blacklistChar.Contains(targetLetter))
                {                   
                    foreach (char countLetter in refWord)
                    {
                        if (targetLetter == countLetter)
                            countRefLetter++;
                    }
                    foreach (char targetCheckLetter in checkWord)
                    {
                        if (!blacklistCharCheck.Contains(targetLetter))
                        {
                            foreach (char _countCheckLetter in checkWord)
                            {
                                if (targetCheckLetter == _countCheckLetter)
                                    countCheckLetter++;
                            }                                                        

                            countCheckLetter = 0;

                            blacklistCharCheck.Add(targetCheckLetter);
                        }
                    }
                    Console.WriteLine($"{countRefLetter}");
                    countRefLetter = 0;

                    blacklistChar.Add(targetLetter);                    
                }                
            }
            blacklistChar.Clear();
            blacklistCharCheck.Clear();

            return true;
        }
    }
}
