using System;
using System.Collections.Generic;

#nullable disable

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class UserLog
    {
        public int UserId { get; set; }
        public string Ip { get; set; }
        public string SearchingWord { get; set; }
        public DateTime SearchTime { get; set; }
        public int FoundAnagramId { get; set; }
    }
}
