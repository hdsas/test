using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransectionController : Controller
    {
        private readonly ILogger<TransectionController> _logger;
        public TransectionController(ILogger<TransectionController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
