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
    public class IndexModel : PageModel
    {
        private readonly StoreServer.Data.StoreServerContext _context;

        public IndexModel(StoreServer.Data.StoreServerContext context)
        {
            _context = context;
        }

        public IList<ItemIdentifier> ItemIdentifier { get;set; }

        public async Task OnGetAsync()
        {
            ItemIdentifier = await _context.ItemIdentifier.ToListAsync();
        }
    }
}
