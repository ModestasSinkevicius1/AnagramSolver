using System;
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
            ContainerSetup(args);            
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
                        AddSingleton<IWordRepository, DBWordRepositoryCodeFirst>().
                        AddSingleton<IAnagramSolver, AnagramSolverLogic>().
                        AddSingleton<IWordService, WordService>().
                        AddSingleton<ConsoleInterface>().
                        Configure<AnagramConfig>(context.Configuration.GetSection(AnagramConfig.Anagram)).
                        Configure<URIConfig>(context.Configuration.GetSection(URIConfig.ClientUriRequest)).
                        Configure<DBConnectionConfig>(context.Configuration.GetSection(DBConnectionConfig.ConnectionStrings)));                            
    }
}
