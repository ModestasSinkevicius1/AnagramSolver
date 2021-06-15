using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Cli
{
    public class ConsoleInterface
    {
        private IAnagramSolver anagramSolver;
        //private List<string> wordList = new List<string>();
        public ConsoleInterface(IAnagramSolver _as)
        {
            anagramSolver = _as;
        }

        public void OutputResult()
        {            
            //Console.WriteLine(anagramSolver.GetAnagrams(GetMyInput()));

            foreach(string ana in anagramSolver.GetAnagrams("labas"))
            {
                Console.WriteLine(ana);
            }
        }
        private string GetMyInput()
        {
            return Console.ReadLine();
        }
    }
}
