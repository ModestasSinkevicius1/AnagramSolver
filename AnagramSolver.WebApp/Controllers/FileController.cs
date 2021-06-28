using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using AnagramSolver.Contracts;
using Microsoft.Extensions.Options;

namespace AnagramSolver.WebApp.Controllers
{
    public class FileController : Controller
    {
        private ContentConfig _contentConfig;
        public FileController(IOptions<ContentConfig> contentConfig)
        {
            _contentConfig = contentConfig.Value;
        }

        public async Task<IActionResult> DownloadDictionary()
        {
            string path = _contentConfig.DictionaryPath;
            MemoryStream memory = new();

            using (FileStream fs = new(path, FileMode.Open))
            {
                await fs.CopyToAsync(memory);
            }
            memory.Position = 0;           

            return File(memory, "text/plain", "zodynas.txt");
        }
    }
}
