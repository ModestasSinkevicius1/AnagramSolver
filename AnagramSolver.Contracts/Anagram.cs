namespace AnagramSolver.Contracts
{
    public class Anagram
    {
        public string Word { get; set; }
        public string Rule { get; set; }
        public string WordRule { get; set; }
        public int Number { get; set; }

        public Anagram(string word, string rule, string wordRule, int number)
        {
            Word = word;
            Rule = rule;
            WordRule = wordRule;
            Number = number;
        }
    }
}
