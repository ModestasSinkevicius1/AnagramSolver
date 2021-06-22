using AnagramSolver.Contracts;
using System.Collections.Generic;

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
           
            if (pageNumber <= 0)
                pageNumber = 1;

            int totalPageSize = pageSize * pageNumber;

            for(int i = totalPageSize - pageSize; i < totalPageSize; i++)
            {
                words.Add(_wordRepository.GetWords()[i].Word);
            }           

            return words;
        }
    }
}
