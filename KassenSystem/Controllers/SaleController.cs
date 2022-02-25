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
    public class SaleController : ControllerBase
    {

      
        private readonly ItemContext _context;
        private readonly ILogger<SaleController> _logger;
        public SaleController(ItemContext context, ILogger<SaleController> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation("init");
        }
        [HttpGet]
        public  async Task<IEnumerable<Sale>> GetAllSales()
        {
            return await _context.SaleModels0.ToListAsync();
        }

        [HttpPost]
        public async Task PostSale(Sale sale)
        {
            _context.SaleModels0.Add(sale);
            await _context.SaveChangesAsync();
        }
        [HttpGet("removeexpired")]
        public async Task DeleteSale()
        {
            foreach (Sale s in await _context.SaleModels0.ToListAsync())
            {
                if ((DateTime.UtcNow - s.PayTime).TotalMinutes > 6)
                {
                    _context.SaleModels0.Remove(s);
                }
                
            }
            await _context.SaveChangesAsync();
        }
        [HttpGet("express")]
        public async Task<bool> GetIsExpress()
        {
            int cash = 0;
            int card = 0;
            foreach(Sale s in await _context.SaleModels0.ToListAsync())
            {
                if(s.PayWay == PayWay.Cash)
                {
                    cash += 1;
                }
                else if (s.PayWay == PayWay.Card)
                {
                    card += 1;
                }
            }
            return (cash>card);
            
        }
        [HttpGet("{id}")]
        public async Task<Sale> GetSale(int id)
        {
            return await _context.SaleModels0.FindAsync(id);

        }


    }
}
