using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using AnagramSolver.Contracts;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IAnagramSolver _anagramSolverLogic;
        private IWordService _wordService;

        public HomeController(ILogger<HomeController> logger, 
            IAnagramSolver anagramSolverLogic,
            IWordService wordService)
        {
            _logger = logger;
            _anagramSolverLogic = anagramSolverLogic;
            _wordService = wordService;
        }

        public IActionResult Index(string myWords)
        {
            if (string.IsNullOrWhiteSpace(myWords))
                return new EmptyResult();

            ViewData["Anagrams"] = _anagramSolverLogic.GetAnagrams(myWords).ToList();           

            return View();
        }

        public IActionResult GetWordFromIndexPage(string word)
        {
            return Redirect($"index?myWords={word}");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Dictionary(int pageNumber, int pageSize)
        {
            ViewData["Words"] = _wordService.GetWords(pageNumber, pageSize);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
