using System;
using System.Collections.Generic;

#nullable disable

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class CachedWord
    {
        public int CachedWordId { get; set; }
        public string SearchingWord { get; set; }
        public int AnagramId { get; set; }

        public virtual Word Anagram { get; set; }
    }
}
