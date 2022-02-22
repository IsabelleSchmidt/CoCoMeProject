using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
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
        public IList<OrderItem> OrderItem { get; set; }
        public Dictionary<int, String> ItemNames { get; set; } = new Dictionary<int, string>();


        public IActionResult OnGet()
        {
            ItemIdentifier = _context.ItemIdentifier.ToList();
            InventoryItem = _context.InventoryItem.ToList();
            OrderItem = _context.OrderItem.ToList().FindAll(orderItem => orderItem.Submitted == false);
            ItemIdentifier.ToList().ForEach(itemIdentifier => ItemNames.Add(itemIdentifier.ID, itemIdentifier.Name));

            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = new Order();



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.OrderItem.ToList().
            _context.Order.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetAddOrderItem(string data)
        {
            ItemIdentifier = _context.ItemIdentifier.ToList();
            InventoryItem = _context.InventoryItem.ToList();
            OrderItem = _context.OrderItem.ToList().FindAll(orderItem => orderItem.Submitted == false);
            ItemIdentifier.ToList().ForEach(itemIdentifier => ItemNames.Add(itemIdentifier.ID, itemIdentifier.Name));
            ItemIdentifier itemIdentifier = _context.ItemIdentifier.ToList().Find(itemIdentifier => itemIdentifier.ID == Convert.ToInt32(data));
            if (itemIdentifier != null && OrderItem.ToList().Find(orderItem => orderItem.ItemIdentifierForeignKey == Convert.ToInt32(data)) == null)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.ItemIdentifierForeignKey = itemIdentifier.ID;
                orderItem.Count = 0;
                orderItem.Submitted = false;
                _context.OrderItem.Add(orderItem);
                await _context.SaveChangesAsync();
                OrderItem = _context.OrderItem.ToList().FindAll(orderItem => orderItem.Submitted == false);
            }
            return Page();
        }

        public async Task<IActionResult> OnGetRemoveOrderItem(string data)
        {
            ItemIdentifier = _context.ItemIdentifier.ToList();
            InventoryItem = _context.InventoryItem.ToList();
            OrderItem = _context.OrderItem.ToList().FindAll(orderItem => orderItem.Submitted == false);
            ItemIdentifier.ToList().ForEach(itemIdentifier => ItemNames.Add(itemIdentifier.ID, itemIdentifier.Name));
            int id = Convert.ToInt32(data);

            OrderItem orderItem = _context.OrderItem.ToList().Find(orderItem => orderItem.ItemIdentifierForeignKey == id);

            if (OrderItem != null)
            {
                _context.OrderItem.RemoveRange(orderItem);
                try
                {
                    await _context.SaveChangesAsync();
                    OrderItem = _context.OrderItem.ToList().FindAll(orderItem => orderItem.Submitted == false);
                }
                catch (Exception)
                {
                    return Page();
                }
            }
            return Page();
        }

    }
}
