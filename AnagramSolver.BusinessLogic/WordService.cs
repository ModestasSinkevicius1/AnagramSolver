using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class WordService : IWordService
    {
        private readonly IWordRepository _wordRepository;

        public WordService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public List<string> GetWords(int pageNumber, int pageSize)
        {
            List<string> words = new List<string>();
            
            int i = 0;

            foreach (Anagram word in _wordRepository.GetWords().Skip(pageNumber * pageSize))
            {
                if (i >= pageSize)
                    break;

                words.Add(word.Word);               
                i++;
            }
            return words;
        }
    }
}
