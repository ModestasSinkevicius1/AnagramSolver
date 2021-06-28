using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Controllers
{
    public class FileController : Controller
    {
        private IConfiguration _config;
        public FileController(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> DownloadDictionary()
        {
            var path = _config["DictionaryPath"];
            var memory = new MemoryStream();
            using(FileStream fs = new FileStream(path, FileMode.Open))
            {
                await fs.CopyToAsync(memory);
            }
            memory.Position = 0;           

            return File(memory, "text/plain", "zodynas.txt");
        }
    }
}
