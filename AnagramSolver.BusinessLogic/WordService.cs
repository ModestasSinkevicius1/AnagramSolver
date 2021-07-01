using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class WordService : IWordService
    {
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramSolver _anagramSolver;

        public WordService(IWordRepository wordRepository, IAnagramSolver anagramSolver)
        {
            _wordRepository = wordRepository;
            _anagramSolver = anagramSolver;
        }

        public List<string> GetWords(int pageNumber, int pageSize, string myWord)
        {
            List<string> words = new();

            if (pageNumber < 0)
                pageNumber = 0;               

            if(pageNumber * pageSize <= 0 && pageSize <= 0)
            {
                pageNumber = 0;
                pageSize = 100;
            }      

            if (string.IsNullOrWhiteSpace(myWord) || myWord == "*")
            {
                words = _wordRepository.GetWords().Select(o => o.Word)
                .Skip(pageSize * pageNumber)
                .Take(pageSize).ToList();

                return words;
            }
        
            List<WordModel> wordObject = _wordRepository.SearchWords(myWord).ToList();

            words = wordObject.Select(o => o.Word)
            .Skip(pageSize * pageNumber)
            .Take(pageSize).ToList();                           
            
            return words;
        }

        public List<string> GetAnagramsByDetermine(string myWord)
        {
            List<string> words;

            if (_wordRepository.CheckCachedWord(myWord))
            {
                words = _wordRepository.GetWordFromCache(myWord).ToList();
                return words;
            }

            words = _anagramSolver.GetAnagrams(myWord).ToList();
            _wordRepository.InsertCachedWord(words, myWord);

            return words;
        }
    }
}
