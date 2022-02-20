using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KassenSystem.Data;
using KassenSystem.Models;
using Microsoft.Extensions.Logging;

namespace KassenSystem.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemContext _context;
        private readonly ILogger<ItemController> _logger;
        private const int ExpressMaxItem = 8;
        public ItemController(ItemContext context, ILogger<ItemController> logger)
        {
            _context = context;
            _logger = logger;
           
        }

        // GET: Item
        public IActionResult Index()
        {
            return View(_context);
        }
        
       

        public async Task<IActionResult> Create(int id)
        {
            var itemModel = await _context.ItemModels.FindAsync(id);
            _logger.LogInformation("create");
            
            if (itemModel != null)
            {
                //var checkoutItemModel = await _context.CheckoutItemModels.F
                    var checkoutItemModel = _context.CheckoutItemModels
                    .Where(b => b.ItemId == id)
                    .FirstOrDefault();
                _logger.LogInformation("found");

                if (checkoutItemModel != null)
                {
                    _logger.LogInformation("add +1");
                    checkoutItemModel.Amount += 1;
                    checkoutItemModel.PriceFull = checkoutItemModel.Amount * checkoutItemModel.PriceSingle;
                   _context.CheckoutItemModels.Update(checkoutItemModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(actionName: "Index", controllerName: "Scanner");
                    
                }
               
                checkoutItemModel = new CheckoutItemModel { Name = itemModel.Name, PriceSingle = itemModel.Price, PriceFull = itemModel.Price, Amount = 1 , ItemId = itemModel.Id};
                
                _context.CheckoutItemModels.Add(checkoutItemModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(actionName: "Index", controllerName: "Scanner");



        }

        public async Task<IActionResult> DeleteLast()
        {
            var lastItem = await _context.CheckoutItemModels.FindAsync(_context.CheckoutItemModels.Max(p => p.Id));
           if(lastItem == null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "CashRegisterSystem");
            }
            if(lastItem.Amount > 1)
            {
                lastItem.Amount -= 1;
                lastItem.PriceFull = lastItem.Amount * lastItem.PriceSingle;
                _context.CheckoutItemModels.Update(lastItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "Index", controllerName: "CashRegisterSystem");
            }
            _context.CheckoutItemModels.Remove(lastItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(actionName: "Index", controllerName: "CashRegisterSystem");
        }

        public async Task<IActionResult> LastPlusOne()
        {
            var lastItem = await _context.CheckoutItemModels.FindAsync(_context.CheckoutItemModels.Max(p => p.Id));
            if (lastItem == null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "CashRegisterSystem");
            }
           
            lastItem.Amount += 1;
            lastItem.PriceFull = lastItem.Amount * lastItem.PriceSingle;
            _context.CheckoutItemModels.Update(lastItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(actionName: "Index", controllerName: "CashRegisterSystem");
          
        }

        public async Task ClearAsync()
        {
            _logger.LogInformation("clear all");
            _context.CheckoutItemModels.RemoveRange(_context.CheckoutItemModels);
            await _context.SaveChangesAsync();
            
        }
        public async Task AddPayment(PayWay payWay)
        {

            _context.PaymentModels.Add(new PaymentModel() { PayTime = DateTime.Now, PayWay = payWay });
            foreach (var p in _context.PaymentModels)
            {
                if (DateTime.Now.Subtract(p.PayTime).TotalMinutes > 1)
                {
                    _context.PaymentModels.Remove(p);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task CheckExpress(PayWay payWay)
        {

            _context.PaymentModels.Add(new PaymentModel() { PayTime = DateTime.Now, PayWay = payWay });
            foreach (var p in _context.PaymentModels)
            {
                if (DateTime.Now.Subtract(p.PayTime).TotalMinutes > 1)
                {
                    _context.PaymentModels.Remove(p);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> endSale()
        {
            await AddPayment(PayWay.Cash);
            await ClearAsync();
            return RedirectToAction(actionName: "Index", controllerName: "CashRegisterSystem");
        }



        // GET: Item/Delete/5


        private bool ItemModelExists(int id)
        {
            return _context.ItemModels.Any(e => e.Id == id);
        }
    }
}
