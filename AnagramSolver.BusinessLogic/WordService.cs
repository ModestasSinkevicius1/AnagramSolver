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
            
            if(string.IsNullOrWhiteSpace(myWord) || myWord == "*")
            {
                words = _wordRepository.GetWords().Select(o => o.Word)
                .Skip(pageSize * pageNumber)
                .Take(pageSize).ToList();

                return words;
            }

            words = _wordRepository.SearchWords(myWord).Select(o => o.Word)
            .Skip(pageSize * pageNumber)
            .Take(pageSize).ToList();

            return words;

        }
    }
}
