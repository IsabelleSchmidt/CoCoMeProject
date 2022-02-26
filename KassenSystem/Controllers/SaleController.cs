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

        [HttpGet("add/{id}")]
        public async Task PostSale(int id)
        {
            var sale = new Sale { PayTime = DateTime.Now, PayWay = id==1 ? PayWay.Cash : PayWay.Card};
            _context.SaleModels0.Add(sale);
            _logger.LogInformation("add " + sale.PayWay.ToString() + sale.PayTime+ "-------------"+id);
            await _context.SaveChangesAsync();
        }
        [HttpGet("removeexpired")]
        public async Task DeleteSale()
        {
            foreach (Sale s in await _context.SaleModels0.ToListAsync())
            {
                _logger.LogInformation(s.PayWay.ToString() + " minutes: " + (DateTime.Now - s.PayTime).TotalMinutes);

                //for test purposes 6 minutes instead of 60
                if ((DateTime.Now - s.PayTime).TotalMinutes > 6)
                {
                    _context.SaleModels0.Remove(s);
                }
                
            }
            await _context.SaveChangesAsync();
        }

        [HttpGet("clear")]
        public async Task Clear()
        {
            if (_context.SaleModels0.Any())
            {
                _context.SaleModels0.RemoveRange(_context.SaleModels0);
                await _context.SaveChangesAsync();
            }
            
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
            if((card+cash) == 0)
            {
                return false;
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
