using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreServer.Data;
using StoreServer.Models;

namespace StoreServer.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly StoreServer.Data.StoreServerContext _context;

        public CreateModel(StoreServer.Data.StoreServerContext context)
        {
            _context = context;
        }

        public IList<ItemIdentifier> ItemIdentifier { get; set; }
        public IList<InventoryItem> InventoryItem { get; set; }
        public Dictionary<int, String> InventoryItemNames { get; set; } = new Dictionary<int, string>();

        public IActionResult OnGet()
        {
            ItemIdentifier = _context.ItemIdentifier.ToList();
            InventoryItem = _context.InventoryItem.ToList();
            InventoryItem.ToList().ForEach(item => InventoryItemNames.Add(item.ItemIdentifierID, _context.ItemIdentifier.ToList().Find((itemIdentifier) => itemIdentifier.ID == item.ItemIdentifierID).Name));
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Order.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
