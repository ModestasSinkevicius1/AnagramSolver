using AnagramSolver.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagramController : ControllerBase
    {
        private IAnagramSolver _anagramSolverLogic;        
        
        public AnagramController(IAnagramSolver anagramSolverLogic)
        {            
            _anagramSolverLogic = anagramSolverLogic;        
        }

        [HttpGet]
        public ActionResult<List<string>> GetAnagrams([FromQuery]string myWord)
        {
            if (string.IsNullOrWhiteSpace(myWord))
                return BadRequest();

            List<string> response = _anagramSolverLogic.GetAnagrams(myWord).ToList();

            return Ok(response);
        }
    }
}
