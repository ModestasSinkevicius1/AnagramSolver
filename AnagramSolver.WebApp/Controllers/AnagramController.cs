using AnagramSolver.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                return StatusCode(400);

            List<string> response = _anagramSolverLogic.GetAnagrams(myWord).ToList();

            return Ok(response);
        }
    }
}
