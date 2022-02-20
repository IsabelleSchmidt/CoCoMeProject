using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KassenSystem.Data;
using KassenSystem.Models;

namespace KassenSystem.Controllers
{
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
            return View(await _context.CheckoutItemModels.ToListAsync());
        }


      


        private bool ItemModelExists(int id)
        {
            return _context.ItemModels.Any(e => e.Id == id);
        }


    }
}
