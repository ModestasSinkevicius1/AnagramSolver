﻿using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class WordService : IWordService
    {
        private readonly IWordRepository _wordRepository;

        public WordService(IEnumerable<IWordRepository> wordRepository)
        {
            _wordRepository = wordRepository.SingleOrDefault(p => p.Key == "Anagram");
        }

        public List<string> GetWords(int pageNumber, int pageSize, string myWord)
        {
            if (pageNumber < 0)
                pageNumber = 0;               

            if(pageNumber * pageSize <= 0 && pageSize <= 0)
            {
                pageNumber = 0;
                pageSize = 100;
            }
            
            var words = _wordRepository.GetWords().Select(o => o.Word)
                .Skip(pageSize * pageNumber)
                .Take(pageSize);

            return words.ToList();           
        }
    }
}
