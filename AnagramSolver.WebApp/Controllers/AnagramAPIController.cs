using AnagramSolver.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Controllers
{
    public class AnagramAPIController : Controller
    {
        private IAnagramSolver _anagramSolverLogic;

        public AnagramAPIController(IAnagramSolver anagramSolverLogic)
        {            
            _anagramSolverLogic = anagramSolverLogic;            
        }
       
        public List<string> GetAnagrams(string myWords)
        {
            if (string.IsNullOrWhiteSpace(myWords))
                return new() { "empty" };           

            return _anagramSolverLogic.GetAnagrams(myWords).ToList();
        }
    }
}
