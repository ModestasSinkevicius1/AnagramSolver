using System;

namespace AnagramSolver.Contracts
{
    public class Anagram
    {
        public string word { get; set; }
        public string rule { get; set; }
        public string wordRule { get; set; }
        public int number { get; set; }
        
        public Anagram(string word, string rule, string wordRule, int number)
        {
            this.word = word;
            this.rule = rule;
            this.wordRule = wordRule;
            this.number = number;
        }
    }
}
