using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KassenSystem.Data;
using KassenSystem.Models;

namespace KassenSystemAngular.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerDisplayController : Controller
    {
        private readonly ItemContext _context;

        public CustomerDisplayController(ItemContext context)
        {
            _context = context;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            float pricesum = _context.CheckoutItemModels.Sum(p => p.PriceFull);
            ViewData["PriceSum"] = pricesum/100;
            return View(await _context.CheckoutItemModels.ToListAsync());
        }


      


        private bool ItemModelExists(int id)
        {
            return _context.ItemModels.Any(e => e.Id == id);
        }


    }
}
