using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using AnagramSolver.Contracts;
using PagedList;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IAnagramSolver _anagramSolverLogic;
        private IWordRepository _wordRepository;

        public HomeController(ILogger<HomeController> logger, 
            IAnagramSolver anagramSolverLogic,
            IWordRepository wordRepository)
        {
            _logger = logger;
            _anagramSolverLogic = anagramSolverLogic;
            _wordRepository = wordRepository;
        }

        public IActionResult Index(string myWords)
        {
            if (string.IsNullOrWhiteSpace(myWords))
                return new EmptyResult();

            ViewData["Anagrams"] = _anagramSolverLogic.GetAnagrams(myWords).ToList();           

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Dictionary()
        {
            ViewData["Words"] = _wordRepository.GetWords();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
