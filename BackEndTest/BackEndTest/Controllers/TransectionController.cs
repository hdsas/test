using BackEndTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransectionController : Controller
    {
        private readonly ILogger<TransectionController> _logger;
        private readonly ICsvService _csvService;
        private readonly string[] FileExtension = { ".CSV", ".XML" };

        public TransectionController(ILogger<TransectionController> logger , ICsvService csvService)
        {
            _logger = logger;
            _csvService = csvService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/upload")]
        public IActionResult Upload() {
            var file = Request.Form.Files[0];
            string extension = Path.GetExtension(file.FileName).ToUpper();
            if (!FileExtension.Contains(extension)) {
                return BadRequest("Unknown Format");
            }


            if (extension == ".CSV" && !_csvService.ExtractData(file,out string errorMessage)) {
                return BadRequest(errorMessage);
            }
               

            return Ok();
        }
    }
}
