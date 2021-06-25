using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Controllers
{
    public class FileController : Controller
    {        
        public async Task<IActionResult> DownloadDictionary()
        {
            var path = @"C:\DownloadableContent\zodynas.txt";
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
