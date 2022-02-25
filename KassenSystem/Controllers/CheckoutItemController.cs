using KassenSystem.BarcodeScannerService;
using KassenSystem.Data;
using KassenSystem.Models;
using KassenSystem.PrintingService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using Tecan.Sila2;
using Tecan.Sila2.Client;
using Tecan.Sila2.Client.ExecutionManagement;
using Tecan.Sila2.Discovery;


namespace KassenSystem.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutItemController : ControllerBase
    {
        private readonly ILogger<CheckoutItemController> _logger;
        private const int ExpressMaxItem = 8;
        private bool scanning;
        private readonly ItemContext _context;

        public CheckoutItemController(ItemContext context, ILogger<CheckoutItemController> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation("init");
            //GetItemsListener();
            scanning = false;



        }
       
        [HttpGet]
        public async Task<IEnumerable<CheckoutItem>> GetAllItems()
        {
            _logger.LogInformation("lalalla");
            return await _context.CheckoutItemModels0.ToListAsync();
        }
        [HttpGet("sum")]
        public async Task<int> GetSum()
        {
            var sum = 0;
            foreach(CheckoutItem i in await _context.CheckoutItemModels0.ToListAsync())
            {
                sum += i.PriceFull;
            }
            return sum;
        }
        [HttpGet("deletelast")]
        public async Task DeleteLast()
        {
            CheckoutItem lastItem = await _context.CheckoutItemModels0.FindAsync(_context.CheckoutItemModels0.Max(p => p.Id));
            if (lastItem != null)
            {

                if (lastItem.Amount > 1)
                {
                    lastItem.Amount -= 1;
                    lastItem.PriceFull = lastItem.Amount * lastItem.PriceSingle;
                    _context.CheckoutItemModels0.Update(lastItem);


                }
                else
                {
                    _context.CheckoutItemModels0.Remove(lastItem);
                }
                await _context.SaveChangesAsync();

            }
        }
        [HttpGet("plusone")]
        public async Task LastPlusOne()
        {
            CheckoutItem lastItem = await _context.CheckoutItemModels0.FindAsync(_context.CheckoutItemModels0.Max(p => p.Id));
            if (lastItem != null)
            {

                lastItem.Amount -= 1;
                lastItem.PriceFull = lastItem.Amount * lastItem.PriceSingle;
                _context.CheckoutItemModels0.Update(lastItem);
                await _context.SaveChangesAsync();

            }
        }
        [HttpDelete]
        public async Task Clear()
        {

            _context.CheckoutItemModels0.RemoveRange(_context.CheckoutItemModels0);
            await _context.SaveChangesAsync();

            
        }

        /*
         public void GetItemsListener()
         {
             var connector = new ServerConnector(new DiscoveryExecutionManager());
             var discovery = new ServerDiscovery(connector);
             var executionManagerFactory = new ExecutionManagerFactory(Enumerable.Empty<IClientRequestInterceptor>());

             var servers = discovery.GetServers(TimeSpan.FromSeconds(10), n => n.NetworkInterfaceType == NetworkInterfaceType.Loopback);

             var terminalServer = servers.First(s => s.Info.Type == "Terminal");
             var bankServer = servers.FirstOrDefault(s => s.Info.Type == "BankServer");

             var terminalServerExecutionManager = executionManagerFactory.CreateExecutionManager(terminalServer);

             var barcodeClient = new BarcodeScannerServiceClient(terminalServer.Channel, terminalServerExecutionManager);
             var readBarcodes = barcodeClient.ListenToBarcodes();
             scanning = true;
             Console.WriteLine("Scanning barcodes");
             Console.WriteLine("Select some items in the terminal application and see the printer adjusting, press Enter to continue");
             PrintNumberOfArticlesScanned(readBarcodes);
         }

         private void SaveArticle(int barcode)
         {
             _checkoutItemRepository.Add(barcode);
             _checkoutItemRepository.Save();
         }



         public async void PrintNumberOfArticlesScanned(IIntermediateObservableCommand<string> barcodes)
         {
             var itemsScanned = 0;
             Console.WriteLine("waiting");
             while (await barcodes.IntermediateValues.WaitToReadAsync() && scanning)
             {
                 Console.WriteLine("...");
                 //await barcodes.IntermediateValues.WaitToReadAsync();
                 if (barcodes.IntermediateValues.TryRead(out var barcode))
                 {
                     //int result = Int32.Parse(barcode);
                     itemsScanned++;
                     Console.WriteLine($"Scanned {barcode} ({itemsScanned} items total)");
                     //SaveArticle(3);
                     //barcodes.Cancel();

                 }
             }
         }
        */
        /*
        [HttpPost]
        [Route("{id:int}", Name = nameof(Create))]
        public async Task<IActionResult> Create(int id)
        {
            _logger.LogInformation("****get one checkout items");
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

                checkoutItemModel = new CheckoutItem { Name = itemModel.Name, PriceSingle = itemModel.Price, PriceFull = itemModel.Price, Amount = 1, ItemId = itemModel.Id };

                _context.CheckoutItemModels.Add(checkoutItemModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(actionName: "Index", controllerName: "Scanner");



        }
        

        public async Task<IActionResult> DeleteLast()
        {
            var lastItem = await _context.CheckoutItemModels.FindAsync(_context.CheckoutItemModels.Max(p => p.ItemId));
            if (lastItem == null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "CashRegisterSystem");
            }
            if (lastItem.Amount > 1)
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
            var lastItem = await _context.CheckoutItemModels.FindAsync(_context.CheckoutItemModels.Max(p => p.ItemId));
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
        */




        /*
        public async Task AddPayment(PayWay payWay)
        {

            _context.PaymentModels.Add(new SaleModel() { PayTime = DateTime.Now, PayWay = payWay });
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

            _context.PaymentModels.Add(new SaleModel() { PayTime = DateTime.Now, PayWay = payWay });
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


        */
        // GET: Item/Delete/5



    }
}
