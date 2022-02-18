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
    public class DeleteModel : PageModel
    {
        private readonly StoreServer.Data.StoreServerContext _context;

        public DeleteModel(StoreServer.Data.StoreServerContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemIdentifier = await _context.ItemIdentifier.FindAsync(id);

            if (ItemIdentifier != null)
            {
                _context.ItemIdentifier.Remove(ItemIdentifier);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return RedirectToPage("./Index");
                }
                
                
            }

            return RedirectToPage("./Index");
        }
    }
}
