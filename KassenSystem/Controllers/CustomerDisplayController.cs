using KassenSystem.Models;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.Extensions.Logging;

namespace KassenSystem.Controllers
{
    public class CustomerDisplayController : Controller
    {
        private readonly ILogger<CustomerDisplayController> _logger;

        public CustomerDisplayController(ILogger<CustomerDisplayController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
