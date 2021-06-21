using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private AnagramSolverLogic _anagramSolverLogic;

        public HomeController(ILogger<HomeController> logger, 
                              AnagramSolverLogic anagramSolverLogic)
        {
            _logger = logger;
            _anagramSolverLogic = anagramSolverLogic;
        }

        public IActionResult Index()
        {
            return View("index", _anagramSolverLogic);
        }

        public IActionResult Privacy()
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
