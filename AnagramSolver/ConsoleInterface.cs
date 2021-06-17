using AnagramSolver.Contracts;
using System;

namespace AnagramSolver.Cli
{
    public class ConsoleInterface
    {
        private IAnagramSolver _anagramSolver;
        
        public ConsoleInterface(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
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

                    if (commandWord != "exit")
                    {
                        foreach (string ana in _anagramSolver.GetAnagrams(commandWord))
                        {
                            Console.WriteLine(ana);
                        }

                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();

                        Console.Clear();
                    }
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
