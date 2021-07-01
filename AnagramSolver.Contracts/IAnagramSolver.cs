using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IAnagramSolver
    {
        IList<WordModel> GetAnagrams(string myWords);
    }
}
