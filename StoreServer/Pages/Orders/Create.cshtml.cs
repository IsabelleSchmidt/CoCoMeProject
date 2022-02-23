using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        [BindProperty]
        public IList<OrderItem> OrderItem { get; set; }
        //public Dictionary<int, String> ItemNames { get; set; } = new Dictionary<int, string>();
        public Order Order { get; set; }

        public IActionResult OnGet()
        {
            ItemIdentifier = _context.ItemIdentifier.ToList();
            InventoryItem = _context.InventoryItem.Include(item => item.ItemIdentifier).ToList();
            OrderItem = _context.OrderItem.ToList().FindAll(orderItem => orderItem.Submitted == false);
            Order = _context.Order.ToList().Find(order => order.Submitted == false);
            if (Order == null) 
            {
                Order = new Order();
                Order.Received = false;
                Order.Submitted = false;
                _context.Order.Add(Order);
                try
                {
                    _context.SaveChanges();
                    Order = _context.Order.ToList().Find(order => order.Submitted == false);
                }
                catch (Exception)
                {
                    return Page();
                }
            }

            return Page();
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int[] Count)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Order = _context.Order.ToList().Find(order => order.Submitted == false);
            Order.Submitted = true;
            Order.OrderDate = DateTime.Now;

            IEnumerator countEnumerator = Count.GetEnumerator();
            List<OrderItem> OrderItemList = _context.OrderItem.Include(orderItem => orderItem.ItemIdentifier).ToList().FindAll(orderItem => orderItem.Submitted == false).ToList();

            OrderItemList.ForEach(orderItem =>
            {
                countEnumerator.MoveNext(); 
                orderItem.Count = (int) countEnumerator.Current;
                
            });
            OrderItemList.ForEach(item => item.Submitted = true);


            _context.Order.Update(Order);
            OrderItemList.ForEach((orderItem)=> _context.OrderItem.Update(orderItem));

            await _context.SaveChangesAsync();

            return RedirectToPage("./OrderIdentifierScreen", new { id = Order.ID });
        }

        public async Task<IActionResult> OnGetAddOrderItem(string data)
        {
            ItemIdentifier = _context.ItemIdentifier.ToList();
            InventoryItem = _context.InventoryItem.Include(item => item.ItemIdentifier).ToList();
            OrderItem = _context.OrderItem.ToList().FindAll(orderItem => orderItem.Submitted == false);
            Order = _context.Order.ToList().Find(order => order.Submitted == false);
            ItemIdentifier itemIdentifier = _context.ItemIdentifier.ToList().Find(itemIdentifier => itemIdentifier.ID == Convert.ToInt32(data));
            if (itemIdentifier != null && OrderItem.ToList().Find(orderItem => orderItem.ItemIdentifier.ID == Convert.ToInt32(data)) == null)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.ItemIdentifier = itemIdentifier;
                orderItem.Count = 0;
                orderItem.Submitted = false;
                orderItem.Order = Order;
                _context.OrderItem.Add(orderItem);
                await _context.SaveChangesAsync();
                OrderItem = _context.OrderItem.ToList().FindAll(orderItem => orderItem.Submitted == false);
            }
            return Page();
        }

        public async Task<IActionResult> OnGetRemoveOrderItem(string data)
        {
            ItemIdentifier = _context.ItemIdentifier.ToList();
            InventoryItem = _context.InventoryItem.Include(item => item.ItemIdentifier).ToList();
            OrderItem = _context.OrderItem.ToList().FindAll(orderItem => orderItem.Submitted == false);
            Order = _context.Order.ToList().Find(order => order.Submitted == false);
            int id = Convert.ToInt32(data);

            OrderItem orderItem = _context.OrderItem.ToList().Find(orderItem => orderItem.Submitted == false && orderItem.ItemIdentifier.ID == id);

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
