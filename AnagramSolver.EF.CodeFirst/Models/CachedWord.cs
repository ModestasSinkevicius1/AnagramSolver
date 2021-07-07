using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class CachedWord
    {
        public int CachedWordID { get; set; }
        public string SearchingWord { get; set; }
        public int WordID { get; set; }

        public virtual WordEnt Word { get; set; }
    }
}
