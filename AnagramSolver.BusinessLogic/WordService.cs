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
            try
            {
                if (pageNumber < 0 || pageSize <= 0)
                    throw new PageNumberOrPageSizeRestrictedException
                        ("Error: Bad page number or page size detected");                

                var words = _wordRepository.GetWords().Select(o => o.Word)
                  .Skip(pageSize * pageNumber)
                  .Take(pageSize);

                return words.ToList();
            }
            catch
            {
                return GetWords(0, 100);
            }
        }
    }
}
