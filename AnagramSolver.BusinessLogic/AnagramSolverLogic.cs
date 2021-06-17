﻿using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolverLogic : IAnagramSolver
    {
        private IWordRepository _wordRepository;

        private readonly AnagramConfig _anagramConfig;    

        public AnagramSolverLogic(IWordRepository wordRepository, IOptions<AnagramConfig> anagramConfig)
        {
            _wordRepository = wordRepository;

            _anagramConfig = anagramConfig.Value;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            IList<string> anagramWords = new List<string>();

            _wordRepository.GetWords();
           
            string wordPattern = $"^[{myWords }]{{{myWords.Length}}}$";

            Regex filterWord = new Regex(wordPattern);

            anagramWords = GetAnagramWords(filterWord, anagramWords, myWords);

            return anagramWords;
        }

        private List<string> GetAnagramWords(Regex filterWord, IList<string> anagramWords, string myWords)
        {
            //Checking if given word matches set of characters                       
            foreach (Anagram ana in _wordRepository.GetWords())
            {
                if (!filterWord.IsMatch(ana.Word))
                {
                    continue;
                }
                if (anagramWords.Contains(ana.Word))
                {
                    continue;
                }
                if (ana.Word == myWords)
                {
                    continue;
                }
                if (IsLetterNotMoreThanGiven(myWords, ana.Word))
                {
                    anagramWords.Add(ana.Word);
                }
            }          

            return anagramWords as List<string>;
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
                if (blacklistChar.Contains(targetLetter))
                {
                    continue;
                }

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

            return true;
        }
    }
}
