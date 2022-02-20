using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreServer.Data;
using StoreServer.Models;

namespace StoreServer.Pages.OrderItems
{
    public class DetailsModel : PageModel
    {
        private readonly StoreServer.Data.StoreServerContext _context;

        public DetailsModel(StoreServer.Data.StoreServerContext context)
        {
            _context = context;
        }

        public OrderItem OrderItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderItem = await _context.OrderItem.FirstOrDefaultAsync(m => m.ID == id);

            if (OrderItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
