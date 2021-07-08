namespace AnagramSolver.Contracts
{
    public class WordModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int Category { get; set; }

        public WordModel(int id, string word, int category)
        {
            Id = id;
            Word = word;
            Category = category;
        }

        public override string ToString() => $"Result: {Word}";        
    }
}
