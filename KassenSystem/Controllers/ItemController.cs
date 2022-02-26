using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KassenSystem.Data;
using KassenSystem.Models;
using Microsoft.Extensions.Logging;

namespace KassenSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly ItemContext _context;
        private readonly ILogger<ItemController> _logger;


        public ItemController(ItemContext context, ILogger<ItemController> logger)
        {
            _context = context;
            _logger = logger;
            

           
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            _logger.LogInformation("item: " + _context.ItemModels.Find(id));
            return await _context.ItemModels.FindAsync(id);
        }
        
        [HttpPost("set")]
        public async Task SetAllItems(Item[] items)
        {
            _logger.LogInformation("setList");

            if (_context.ItemModels.Any<Item>())
            {
                _context.ItemModels.RemoveRange(_context.ItemModels);
              
            }
            foreach (Item item in items)
            {
                _context.ItemModels.Add(item);
            }
            await _context.SaveChangesAsync();
            
        }

        
    }
}
