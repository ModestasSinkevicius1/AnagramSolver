using AnagramSolver.Contracts;
using AnagramSolver.BusinessLogic;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace AnagramSolver.Cli
{
    public class ConsoleInterface
    {
        private readonly IAnagramSolver _anagramSolver;

        private static readonly HttpClient client = new HttpClient();

        private readonly URIConfig _uriConfig;

        public ConsoleInterface(IAnagramSolver anagramSolver, IOptions<URIConfig> uriConfig)
        {
            _anagramSolver = anagramSolver;
            _uriConfig = uriConfig.Value;
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
                        commandWord = Console.ReadLine();
                        
                        RequestToServer(commandWord).Wait();             

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
                var builder = new UriBuilder(_uriConfig.Uri);

                builder.Query = $"myWord={myWord}";

                HttpResponseMessage response = await client.GetAsync(builder.Uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                List<string> words = JsonConvert.DeserializeObject<List<string>>(responseBody);

                foreach(string word in words)
                {
                    Console.WriteLine(word);
                }               
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
