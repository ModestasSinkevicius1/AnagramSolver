using AnagramSolver.Contracts;
using AnagramSolver.BusinessLogic;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;
using System.Linq;

namespace AnagramSolver.Cli
{
    public class ConsoleInterface
    {
        private readonly IAnagramSolver _anagramSolver;

        static readonly HttpClient client = new HttpClient();

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

                    if (commandWord != "exit" && commandWord != "http")
                    {
                        Console.WriteLine("Getting anagrams...");

                        foreach (string ana in _anagramSolver.GetAnagrams(commandWord))
                        {
                            Console.WriteLine(ana);
                        }
                        
                        OutputMessage("Press enter to continue");
                    }
                    if(commandWord == "http")
                    {
                        Console.WriteLine("Type here a request");
                        //commandWord = Console.ReadLine();
                        
                        RequestToServer("labas").Wait();             

                        OutputMessage("Press enter to continue");
                    }
                }
            }
            catch (WordIsEmptyException exc)
            {
                OutputMessage($"{ exc.Message } \nPress enter to continue");
                OutputResult();
            }
            catch (WordTooLongException exc)
            {
                OutputMessage($"{ exc.Message } \nPress enter to continue");
                OutputResult();
            }
            catch (Exception exc)
            {
                OutputMessage(exc.Message);                                                                        
            }         
        }

        async Task RequestToServer(string myWord)
        {
            Console.WriteLine("Connecting...");

            try
            {
                var builder = new UriBuilder("http://localhost:8080/api/anagram");

                builder.Query = $"myWoRD={myWord}";

                HttpResponseMessage response = await client.GetAsync(builder.Uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"\nMessage :{e.Message}");
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
