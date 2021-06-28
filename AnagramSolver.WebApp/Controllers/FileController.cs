using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            var path = $"{_contentConfig.DictionaryPath}{_contentConfig.ContentName}";
            var memory = new MemoryStream();
            using(FileStream fs = new FileStream(path, FileMode.Open))
            {
                await fs.CopyToAsync(memory);
            }
            memory.Position = 0;           

            return File(memory, "text/plain", _contentConfig.ContentName);
        }
    }
}
