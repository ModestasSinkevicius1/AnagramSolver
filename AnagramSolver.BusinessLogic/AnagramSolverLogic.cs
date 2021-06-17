﻿using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolverLogic : IAnagramSolver
    {
        private IWordRepository _wordRepository;

        //private readonly AnagramConfig _anagramConfig;    

        /*Removed temporary AnagramConfig since it's
          causing unit test not to work properly and currenty 
          not usable in methods.*/
        public AnagramSolverLogic(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;

            //_anagramConfig = anagramConfig.Value;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            IList<string> anagramWords = new List<string>();

            _wordRepository.GetWords();
           
            string wordPattern = "^" + "[" + myWords + "]" + "{" + myWords.Length + "}" + "$";            

            Regex filterWord = new Regex(wordPattern);           

            //Checking if given word matches set of characters                       
            foreach (Anagram ana in _wordRepository.GetWords())
            {
                if (filterWord.IsMatch(ana.Word))
                {
                    if(!anagramWords.Contains(ana.Word) && ana.Word != myWords)
                        if(IsLetterNotMoreThanGiven(myWords, ana.Word))
                            anagramWords.Add(ana.Word);
                }
            }           

            return anagramWords;
        }

        /*checking if anagram has exact letters as given word input.

            "refWord" is our given input.
            "checkWord" is anagram. 
        */
        private bool IsLetterNotMoreThanGiven(string refWord, string checkWord)
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
