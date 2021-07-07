﻿using AnagramSolver.Contracts;
using AnagramSolver.BusinessLogic;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using AnagramSolver.EF.CodeFirst.DAL;
using AnagramSolver.EF.CodeFirst.Models;

namespace AnagramSolver.Cli
{
    public class ConsoleInterface
    {
        private readonly IAnagramSolver _anagramSolver;

        private static readonly HttpClient client = new HttpClient();

        private readonly URIConfig _uriConfig;

        private readonly DBConnectionConfig _dbConConfig;

        private readonly IWordRepository _wordRepository;

        private readonly IWordService _wordService;

        public ConsoleInterface(IAnagramSolver anagramSolver, IOptions<URIConfig> uriConfig,
            IOptions<DBConnectionConfig> dbConConfig, IWordRepository wordRepository,
            IWordService wordService)
        {
            _anagramSolver = anagramSolver;
            _uriConfig = uriConfig.Value;
            _dbConConfig = dbConConfig.Value;
            _wordRepository = wordRepository;
            _wordService = wordService;
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

                    if (commandWord != "exit" && commandWord != "http" && commandWord != "delete")
                    {
                        Console.WriteLine("Getting anagrams...");

                        foreach (WordModel ana in _wordService.GetAnagramsByQuery(commandWord))
                        {
                            Console.WriteLine(ana.ToString());
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
                    if(commandWord == "delete")
                    {
                        Console.WriteLine("Type word that will be deleted");
                        commandWord = Console.ReadLine();

                        _wordRepository.DeleteRecordFromWordTable(commandWord);

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

        private void StoreDataToDB()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = _dbConConfig.DBConnection;
            cn.Open();

            SqlCommand cmd;            

            foreach (WordModel ana in _wordRepository.GetWords().
                GroupBy(o => o.Word).Select(group => group.First()))
            {
                cmd = new();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "WordInsert";
                cmd.Parameters.Add(new SqlParameter("@Word", ana.Word));
                cmd.Parameters.Add(new SqlParameter("@Category", ana.Category));
                cmd.ExecuteNonQuery();
            }

            cn.Close();
        }

        private void StoreDataToDBWithEF()
        {
            using(var db = new AnagramDBCodeFirstContext())
            {
                List<WordModel> words = _wordRepository.GetWords().ToList();

                int countWord = 0;

                foreach (WordModel ana in _wordRepository.GetWords().
                GroupBy(o => o.Word).Select(group => group.First()))
                {
                    var wordEntity = new WordEnt() 
                    { 
                        Word = ana.Word,
                        Category = ana.Category
                    };

                    db.Word.Add(wordEntity);                               
                    Console.Write($"\r{countWord++}/{words.Count-1}");               
                }
                db.SaveChanges();
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
