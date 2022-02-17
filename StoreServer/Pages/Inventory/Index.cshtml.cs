using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreServer.Data;
using StoreServer.Models;

namespace StoreServer.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        private readonly StoreServer.Data.StoreServerContext _context;

        public IndexModel(StoreServer.Data.StoreServerContext context)
        {
            _context = context;
        }

        public IList<InventoryItem> InventoryItem { get;set; }
        public Dictionary<int, String> InventoryItemNames { get; set; } = new Dictionary<int, string>();


        public async Task OnGetAsync()
        {
            InventoryItem = await _context.InventoryItem.ToListAsync();
            InventoryItem.ToList().ForEach(item => InventoryItemNames.Add(item.ItemIdentifierID, _context.ItemIdentifier.ToList().Find((itemIdentifier) => itemIdentifier.ID == item.ItemIdentifierID).Name));
        }
    }
}
