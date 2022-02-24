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

namespace StoreServer.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly StoreServer.Data.StoreServerContext _context;

        public EditModel(StoreServer.Data.StoreServerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        public IList<OrderItem> OrderItem { get; set; }
        public IList<InventoryItem> InventoryItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order.FirstOrDefaultAsync(m => m.ID == id);
            OrderItem = _context.OrderItem.Include(item => item.Order).Include(item => item.ItemIdentifier).ToList().FindAll(orderItem => orderItem.Order.ID == Order.ID);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnGetAcceptOrder(int orderId)
        {
            Order = await _context.Order.FirstOrDefaultAsync(m => m.ID == orderId);
            OrderItem = _context.OrderItem.Include(item => item.Order).Include(item => item.ItemIdentifier).ToList().FindAll(orderItem => orderItem.Order.ID == Order.ID);
            InventoryItem = _context.InventoryItem.Include(item => item.ItemIdentifier).ToList();

            Order.Received = true;
            Order.RecieveDate = DateTime.Now;
            _context.Order.Update(Order);

            OrderItem.ToList().ForEach(orderItem => {
                InventoryItem existingInventoryItem = InventoryItem.ToList().Find(inventoryItem => inventoryItem.ItemIdentifier.ID == orderItem.ItemIdentifier.ID);
                if (existingInventoryItem != null) 
                {
                    existingInventoryItem.Count += orderItem.Count;
                } else
                {
                    InventoryItem newInventoryItem = new InventoryItem();
                    newInventoryItem.ItemIdentifier = orderItem.ItemIdentifier;
                    newInventoryItem.Count = orderItem.Count;
                    newInventoryItem.Price = 0;
                    _context.InventoryItem.Add(newInventoryItem);
                }
            });

            await _context.SaveChangesAsync();
            OrderItem = _context.OrderItem.ToList().FindAll(orderItem => orderItem.Submitted == false);
            return RedirectToPage("./Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.ID == id);
        }
    }
}
