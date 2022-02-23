using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreServer.Data;
using StoreServer.Models;

namespace StoreServer.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly StoreServer.Data.StoreServerContext _context;


        public DetailsModel(StoreServer.Data.StoreServerContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }
        public IList<OrderItem> OrderItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order.FirstOrDefaultAsync(m => m.ID == id);
            OrderItem = _context.OrderItem.Include(item => item.Order).Include(item => item.ItemIdentifier).ToList().FindAll(orderItem => orderItem.Order.ID == Order.ID);
            
            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
