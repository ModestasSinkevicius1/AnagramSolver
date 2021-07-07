using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class UserLog
    {
        public int UserLogID { get; set; }
        public string Ip { get; set; }
        public string SearcingWord { get; set; }
        public DateTime SearchTime { get; set; }
        public int WordID { get; set; }

        public virtual WordEnt Word { get; set; }
    }
}
