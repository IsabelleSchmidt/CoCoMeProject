using KassenSystem.Models;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.Extensions.Logging;
using KassenSystem.Data;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KassenSystem.Controllers
{
    public class ScannerController : Controller
    {
        // GET: /<controller>/
        private readonly ILogger<ScannerController> _logger;

        private readonly ItemContext _context;

        



        public ScannerController(ILogger<ScannerController> logger, ItemContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemModels.ToListAsync());
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

