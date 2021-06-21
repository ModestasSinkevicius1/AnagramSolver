using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //private IAnagramSolver _anagramSolverLogic;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_anagramSolverLogic = anagramSolverLogic;
        }

        public IActionResult Index([FromServices] IAnagramSolver anagramSolverLogic)
        {                 
            ViewData["Anagram"] = anagramSolverLogic.GetAnagrams("labas")[0];

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
