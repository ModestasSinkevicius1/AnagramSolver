using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolverLogic : IAnagramSolver
    {
        private readonly IWordRepository _wordRepository;

        private readonly AnagramConfig _anagramConfig;    

        public AnagramSolverLogic(IWordRepository wordRepository, IOptions<AnagramConfig> anagramConfig)
        {
            _wordRepository = wordRepository;

            _anagramConfig = anagramConfig.Value;
        }

        public IList<string> GetAnagrams(string myWords)
        {           
            if (myWords == "")
                throw new WordIsEmptyException("word was empty");

            if (_anagramConfig.MinWordLength < myWords.Length)
                throw new WordTooLongException("input word too long");

            string wordPattern = $"^[{ myWords }]{{{ myWords.Length }}}$";

            Regex filterWord = new Regex(wordPattern);

            List<string> anagramWords = GetAnagramWords(filterWord, myWords);

            return anagramWords;
        }

        private List<string> GetAnagramWords(Regex filterWord, string myWords)
        {
            HashSet<string> anagramWords = new HashSet<string>();
            
            //Checking if given word matches set of characters                       
            foreach (Anagram ana in _wordRepository.GetWords())
            {
                if(anagramWords.Count >= _anagramConfig.TotalOutputAnagrams)
                {
                    break;
                }
                if (!filterWord.IsMatch(ana.Word))
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

            return anagramWords.ToList();
        }

        /*checking if anagram has exact letters as given word input.

            "refWord" is our given input.
            "checkWord" is anagram. 
        */
        private bool IsLetterNotMoreThanGiven(string refWord, string checkWord)
        {
            int countRefLetter = 0;
            int countCheckLetter = 0;                        

            foreach(char targetLetter in refWord)
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
            }            

            return true;
        }
    }
}
