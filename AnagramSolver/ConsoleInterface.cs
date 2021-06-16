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
        
        public ConsoleInterface(IAnagramSolver _as)
        {
            anagramSolver = _as;
        }

        public void OutputResult()
        {
            try
            {
                string commandWord = "";                

                while (commandWord != "exit")
                {
                    Console.WriteLine("Type 'exit' or press Ctrl + C to close program");

                    commandWord = GetMyInput();

                    foreach (string ana in anagramSolver.GetAnagrams(commandWord))
                    {
                        Console.WriteLine(ana);
                    }                  

                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();

                    Console.Clear();                 
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        private string GetMyInput()
        {
            return Console.ReadLine();
        }
    }
}
