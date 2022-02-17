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

namespace StoreServer.Pages.Inventory
{
    public class EditModel : PageModel
    {
        private readonly StoreServer.Data.StoreServerContext _context;

        public EditModel(StoreServer.Data.StoreServerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InventoryItem InventoryItem { get; set; }
        public String InventoryItemName { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InventoryItem = await _context.InventoryItem.FirstOrDefaultAsync(m => m.ID == id);
            InventoryItemName = _context.ItemIdentifier.ToList().Find((itemIdentifier) => itemIdentifier.ID == InventoryItem.ItemIdentifierID).Name;
            if (InventoryItem == null)
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

            _context.Attach(InventoryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemExists(InventoryItem.ID))
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

        private bool InventoryItemExists(int id)
        {
            return _context.InventoryItem.Any(e => e.ID == id);
        }
    }
}
