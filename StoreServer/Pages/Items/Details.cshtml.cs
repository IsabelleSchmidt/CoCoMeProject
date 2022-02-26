using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreServer.Data;
using StoreServer.Models;

namespace StoreServer.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly StoreServer.Data.StoreServerContext _context;

        public DetailsModel(StoreServer.Data.StoreServerContext context)
        {
            _context = context;
        }

        public ItemIdentifier ItemIdentifier { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemIdentifier = await _context.ItemIdentifier.FirstOrDefaultAsync(m => m.ID == id);

            if (ItemIdentifier == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
