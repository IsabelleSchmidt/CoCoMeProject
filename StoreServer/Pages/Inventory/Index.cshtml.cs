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


        public async Task OnGetAsync()
        {
            InventoryItem = await _context.InventoryItem.Include(item => item.ItemIdentifier).ToListAsync();
        }

        public IActionResult OnGetCreateReport()
        {
            /* Render URL to PDF anchor-convert-website-to-pdf
             * speichert pdf in ordnerstruktur
             */
            var render = new IronPdf.ChromePdfRenderer();
            var doc = render.RenderUrlAsPdf("https://localhost:7071/Inventory");
            doc.SaveAs("InventoryReport.pdf");
            return RedirectToAction(actionName: "https://localhost:7071/Inventory");

        }
    }
}
