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
    public class DeleteModel : PageModel
    {
        private readonly StoreServer.Data.StoreServerContext _context;

        public DeleteModel(StoreServer.Data.StoreServerContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderItem = await _context.OrderItem.FindAsync(id);

            if (OrderItem != null)
            {
                _context.OrderItem.Remove(OrderItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
