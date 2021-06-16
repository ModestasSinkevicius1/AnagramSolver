using System;
using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolverLogic : IAnagramSolver
    {
        private IWordRepository _wordRepository;
              
        public AnagramSolverLogic(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            IList<string> anagramWordList = new List<string>();

            _wordRepository.GetWords();
           
            string wordPattern = "^" + "[" + myWords + "]" + "{" + myWords.Length + "}" + "$";

            Regex filterWord = new Regex(wordPattern);           

            //Checking if given word matches set of characters                       
            foreach (Anagram ana in _wordRepository.GetWords())
            {
                if (filterWord.IsMatch(ana.Word))
                {
                    if(!anagramWordList.Contains(ana.Word) && ana.Word != myWords)
                        if(IsLetterNotMoreThanGiven(myWords, ana.Word))
                            anagramWordList.Add(ana.Word);
                }
            }           

            return anagramWordList;
        }

        /*checking if anagram has exact letters as given word input.

            "refWord" is our given input.
            "checkWord" is anagram. 
        */
        public bool IsLetterNotMoreThanGiven(string refWord, string checkWord)
        {
            int countRefLetter = 0;
            int countCheckLetter = 0;

            List<char> blacklistChar = new List<char>();            

            foreach(char targetLetter in refWord)
            {                
                if (!blacklistChar.Contains(targetLetter))
                {                    
                    foreach (char refLetter in refWord)
                    {
                        if (targetLetter == refLetter)
                            countRefLetter++;
                    }

                    foreach (char checkLetter in checkWord)
                    {
                        if (targetLetter == checkLetter)
                            countCheckLetter++;
                    }

                    if (countRefLetter != countCheckLetter)
                        return false;

                    countRefLetter = 0;
                    countCheckLetter = 0;

                    blacklistChar.Add(targetLetter);                
                }                
            }            

            return true;
        }
    }
}
