using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class WordEnt
    {
        public int ID { get; set; }
        public string Word { get; set; }
        public int Category { get; set; }

        public virtual ICollection<CachedWord> CachedWords { get; set; }
        public virtual ICollection<UserLog> UserLogs { get; set; }
    }
}
