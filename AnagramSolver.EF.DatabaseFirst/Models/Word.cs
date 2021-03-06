using System;
using System.Collections.Generic;

#nullable disable

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class Word
    {
        public Word()
        {
            CachedWords = new HashSet<CachedWord>();
            UserLogs = new HashSet<UserLog>();
        }

        public int Id { get; set; }
        public string Word1 { get; set; }
        public int Category { get; set; }

        public virtual ICollection<CachedWord> CachedWords { get; set; }
        public virtual ICollection<UserLog> UserLogs { get; set; }
    }
}
