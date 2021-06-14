using System;
using System.Text.RegularExpressions;

namespace AnagramSolver.Cli
{
    class Program
    {
        static void Main(string[] args)
        {         
            Regex filterWord = new Regex(@"[balas]{4}");

            //string keyWord = Console.ReadLine();

            string keyWord = "labas";

            //Checking if given word matches set of characters
            if (filterWord.IsMatch(keyWord))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            
        }
    }
}
