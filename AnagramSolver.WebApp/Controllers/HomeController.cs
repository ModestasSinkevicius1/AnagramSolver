using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using AnagramSolver.Contracts;
using System.Collections.Generic;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly IWordService _wordService;
        private readonly IUserLogService _userService;

        public HomeController(ILogger<HomeController> logger, 
            IAnagramSolver anagramSolverLogic,
            IWordService wordService,
            IUserLogService userService)
        {
            _logger = logger;            
            _wordService = wordService;
            _userService = userService;
        }

        public IActionResult Index(string myWords)
        {
            if (string.IsNullOrWhiteSpace(myWords))
                return new EmptyResult();

            List<WordModel> words = _wordService.GetAnagramsByQuery(myWords);

            ViewData["Anagrams"] = words.ToList();

            _userService.InsertUserLog(words, myWords,
                Request.HttpContext.Connection.RemoteIpAddress.ToString());

            return View();
        }

        public IActionResult Privacy() => View();       

        public IActionResult Dictionary(int pageNumber, int pageSize, string myWord)
        {
            ViewData["Words"] = _wordService.GetWords(pageNumber, pageSize, myWord);
            return View();
        }

        public IActionResult FileAccess() => View();

        public IActionResult UserLog()
        {
            ViewData["Users"] = _userService.GetUserLog().ToList();
            return View();
        }

        public IActionResult SearchWord(string searchWord)
        {
            return RedirectToAction("Dictionary", new {myWord=searchWord});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
