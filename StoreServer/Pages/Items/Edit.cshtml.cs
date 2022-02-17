using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreServer.Data;
using StoreServer.Models;

namespace StoreServer.Pages.Items
{
    public class EditModel : PageModel
    {
        private readonly StoreServer.Data.StoreServerContext _context;

        public EditModel(StoreServer.Data.StoreServerContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ItemIdentifier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemIdentifierExists(ItemIdentifier.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ItemIdentifierExists(int id)
        {
            return _context.ItemIdentifier.Any(e => e.ID == id);
        }
    }
}
