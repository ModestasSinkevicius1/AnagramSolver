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
            //List<string> words = _wordRepository.GetWords().SelectMany(o => o.Word);
            
            var words = _wordRepository.GetWords().Select(o => o.Word)
              .Skip(pageSize * pageNumber)
              .Take(pageSize);

            return words.ToList();
        }
    }
}
