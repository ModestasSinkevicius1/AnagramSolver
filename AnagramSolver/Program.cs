using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;

namespace AnagramSolver.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            Console.OutputEncoding = System.Text.Encoding.UTF8;           
            
            /*foreach(Anagram ana in GetWords())
            {
                Console.WriteLine(ana.word);
            }*/

            /*Regex filterWord = new Regex(@"[balas]{4}");

            //string keyWord = Console.ReadLine();

            string keyWord = "labas";

            //Checking if given word matches set of characters
            if (filterWord.IsMatch(keyWord))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            */

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            ConsoleInterface ci = provider.GetRequiredService<ConsoleInterface>();

            ci.OutputResult();
            
            host.Run();
        }
        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.
                        AddSingleton<IWordRepository, AnagramSolverWordRepository>().
                        AddSingleton<IAnagramSolver, AnagramSolverLogic>().
                        AddSingleton<ConsoleInterface>());
                            
    }
}
