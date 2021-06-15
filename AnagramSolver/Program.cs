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
        {S
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            AnagramSolverWordRepository aswr = new AnagramSolverWordRepository();

            foreach(Anagram ana in aswr.GetWords())
            {
                Console.WriteLine(ana.word);
            }

            /*Regex filterWord = new Regex(@"[balas]{4}");

            //string keyWord = Console.ReadLine();

            string keyWord = "labas";

            //Checking if given word matches set of characters
            if (filterWord.IsMatch(keyWord))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            */
            
        }
    }
}
