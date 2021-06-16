using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IAnagramSolver
    {
        IList<string> GetAnagrams(string myWords);

        bool IIsLetterNotMoreThanGiven(string refWord, string checkWord);
    }
}
