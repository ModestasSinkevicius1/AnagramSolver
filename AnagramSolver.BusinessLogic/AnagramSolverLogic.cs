using System;
using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolverLogic : IAnagramSolver
    {
        private IWordRepository iwr;
              
        public AnagramSolverLogic(IWordRepository iwr)
        {
            this.iwr = iwr;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            IList<string> aList = new List<string>();

            iwr.GetWords();
           
            string wordPattern = "^" + "[" + myWords + "]" + "{" + myWords.Length + "}" + "$";

            Regex filterWord = new Regex(wordPattern);           

            //Checking if given word matches set of characters                       
            foreach (Anagram ana in iwr.GetWords())
            {
                if (filterWord.IsMatch(ana.Word))
                {
                    if(!aList.Contains(ana.Word) && ana.Word != myWords)
                        if(IsLetterNotMoreThanGiven(myWords, ana.Word))
                            aList.Add(ana.Word);
                }
            }           

            return aList;
        }

        public bool IsLetterNotMoreThanGiven(string refWord, string checkWord)
        {
            int countR = 0;
            int countC = 0;

            List<char> blacklistChar = new List<char>();            

            foreach(char targetLetter in refWord)
            {                
                if (!blacklistChar.Contains(targetLetter))
                {                    
                    foreach (char countRefLetter in refWord)
                    {
                        if (targetLetter == countRefLetter)
                            countR++;
                    }

                    foreach (char countCheckLetter in checkWord)
                    {
                        if (targetLetter == countCheckLetter)
                            countC++;
                    }

                    if (countR != countC)
                        return false;

                    countR = 0;
                    countC = 0;

                    blacklistChar.Add(targetLetter);                
                }                
            }            

            return true;
        }
    }
}
