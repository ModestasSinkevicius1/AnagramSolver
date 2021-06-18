using AnagramSolver.Contracts;
using AnagramSolver.BusinessLogic;
using System;

namespace AnagramSolver.Cli
{
    public class ConsoleInterface
    {
        private readonly IAnagramSolver _anagramSolver;
        
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
                        
                        OutputMessage("Press enter to continue");
                    }
                }
            }
            catch (WordIsEmptyException exc)
            {
                OutputMessage(exc.Message);
                OutputResult();
            }
            catch (WordTooLongException exc)
            {
                OutputMessage(exc.Message);
                OutputResult();
            }
            catch (Exception exc)
            {
                OutputMessage(exc.Message);                                                                        
            }         
        }

        private void OutputMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();

            Console.Clear();
        }
        private string GetMyInput()
        {
            return Console.ReadLine();
        }
    }
}
