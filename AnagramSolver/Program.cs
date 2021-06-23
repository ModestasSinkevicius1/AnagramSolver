using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnagramSolver.Cli
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main()
        {
            Console.WriteLine("Connecting...");
            //ContainerSetup(args);
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44357/AnagramAPI/GetAnagram/");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();               

                Console.WriteLine(response.StatusCode);
            }
            catch (HttpRequestException e)
            {   
                Console.WriteLine($"\nMessage :{e.Message}");
            }
        }

        static void ContainerSetup(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            ConsoleInterface ci = provider.GetRequiredService<ConsoleInterface>();

            ci.OutputResult();

            host.Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                    services.
                        AddSingleton<IWordRepository, AnagramSolverWordRepository>().
                        AddSingleton<IAnagramSolver, AnagramSolverLogic>().
                        AddSingleton<ConsoleInterface>().
                        Configure<AnagramConfig>(context.Configuration.GetSection(AnagramConfig.Anagram)));
                            
    }
}
