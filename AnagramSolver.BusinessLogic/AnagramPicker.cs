using AnagramSolver.Contracts;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramPicker
    {
        private IAnagramSolver _anagramSolver;
        private IWordRepository _wordRepository;
        public AnagramPicker(IAnagramSolver anagramSolver, IWordRepository wordRepository)
        {
            _anagramSolver = anagramSolver;
            _wordRepository = wordRepository;
        }

        public List<string> DetermineWhereToPick(string myWord)
        {
            List<string> words = new();

            _anagramSolver.GetAnagrams(myWord);

            

            return words;
        }
    }
}
