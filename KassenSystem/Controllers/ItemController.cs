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

using System.Net.NetworkInformation;
using Tecan.Sila2;
using Tecan.Sila2.Client;
using Tecan.Sila2.Client.ExecutionManagement;
using Tecan.Sila2.Discovery;
using KassenSystem;
using KassenSystem.BarcodeScannerService;
using KassenSystem.CashboxService;
using KassenSystem.DisplayController;
using KassenSystem.PrintingService;
/*
var connector = new ServerConnector(new DiscoveryExecutionManager());
var discovery = new ServerDiscovery(connector);
var executionManagerFactory = new ExecutionManagerFactory(Enumerable.Empty<IClientRequestInterceptor>());

var servers = discovery.GetServers(TimeSpan.FromSeconds(10), n => n.NetworkInterfaceType == NetworkInterfaceType.Loopback);

var terminalServer = servers.First(s => s.Info.Type == "Terminal");
var bankServer = servers.FirstOrDefault(s => s.Info.Type == "BankServer");

var terminalServerExecutionManager = executionManagerFactory.CreateExecutionManager(terminalServer);

// demo of cashbox and display
var cashboxClient = new CashboxServiceClient(terminalServer.Channel, terminalServerExecutionManager);
var displayClient = new DisplayControllerClient(terminalServer.Channel, terminalServerExecutionManager);
var cashboxButtons = cashboxClient.ListenToCashdeskButtons();
Console.WriteLine("Reading cashbox buttons");
Console.WriteLine("Press some of the cashbox buttons and see how the display adjust. Press Enter to continue");
var listenToCashBoxButtons = cashboxClient.ListenToCashdeskButtons();
DisplayButtonsPressed(displayClient, listenToCashBoxButtons);
//Console.ReadLine();
listenToCashBoxButtons.Cancel();

// demo of printer and barcode reader
var printerClient = new PrintingServiceClient(terminalServer.Channel, terminalServerExecutionManager);
var barcodeClient = new BarcodeScannerServiceClient(terminalServer.Channel, terminalServerExecutionManager);
var readBarcodes = barcodeClient.ListenToBarcodes();
Console.WriteLine("Scanning barcodes");
Console.WriteLine("Select some items in the terminal application and see the printer adjusting, press Enter to continue");
PrintNumberOfArticlesScanned(printerClient, readBarcodes);
Console.ReadLine();
readBarcodes.Cancel();

static async void PrintNumberOfArticlesScanned(IPrintingService printer, IIntermediateObservableCommand<string> barcodes)
{
    var itemsScanned = 0;
    while (await barcodes.IntermediateValues.WaitToReadAsync())
    {
        if (barcodes.IntermediateValues.TryRead(out var barcode))
        {
            itemsScanned++;
            printer.PrintLine(barcode);
            Console.WriteLine($"Scanned {barcode} ({itemsScanned} items total)");
        }
    }
}
static async void DisplayButtonsPressed(IDisplayController display, IIntermediateObservableCommand<CashboxButton> cashboxButtons)
{
    while (await cashboxButtons.IntermediateValues.WaitToReadAsync())
    {
        if (cashboxButtons.IntermediateValues.TryRead(out var button))
        {
            display.SetDisplayText($"{button} pressed");
        }
    }
}
*/
namespace KassenSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            _logger.LogInformation("item: " + _context.ItemModels.Find(id));
            return await _context.ItemModels.FindAsync(id);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            _logger.LogInformation("lalalla");
            return await _context.ItemModels.ToListAsync();
        }
        
        [HttpPost]
        public async Task PostItem([FromBody] Item item)
        {
            _logger.LogInformation("item: "+item.Name);

            
            //var checkoutItemModel = await _context.CheckoutItemModels.F
            var checkoutItemModel = _context.CheckoutItemModels0
            .Where(b => b.ItemId == item.Id)
            .FirstOrDefault();
            _logger.LogInformation("found");

            if (checkoutItemModel != null)
            {
                _logger.LogInformation("add +1");
                checkoutItemModel.Amount += 1;
                checkoutItemModel.PriceFull = checkoutItemModel.Amount * checkoutItemModel.PriceSingle;
                _context.CheckoutItemModels0.Update(checkoutItemModel);
                await _context.SaveChangesAsync();
                //return await _context.CheckoutItemModels0.FindAsync(item.Id);

            }
            else
            {
                checkoutItemModel = new CheckoutItem { Name = item.Name, PriceSingle = item.Price, PriceFull = item.Price, Amount = 1, ItemId = item.Id };
                _logger.LogInformation(checkoutItemModel.Name + " : " + checkoutItemModel.Id);

                _context.CheckoutItemModels0.Add(checkoutItemModel);
                await _context.SaveChangesAsync();
            }

           
           

            //return await _context.CheckoutItemModels0.FindAsync(item.Id);
        }

        [HttpPost("/{id}")]
        public async Task PostById(int id)
        {
            _logger.LogInformation("item: " + id);
             Item item = await _context.ItemModels.FindAsync(id);
            if(item != null)
            {
                var checkoutItemModel = _context.CheckoutItemModels0
            .Where(b => b.ItemId == item.Id)
            .FirstOrDefault();
                _logger.LogInformation("found");

                if (checkoutItemModel != null)
                {
                    _logger.LogInformation("add +1");
                    checkoutItemModel.Amount += 1;
                    checkoutItemModel.PriceFull = checkoutItemModel.Amount * checkoutItemModel.PriceSingle;
                    _context.CheckoutItemModels0.Update(checkoutItemModel);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    checkoutItemModel = new CheckoutItem { Name = item.Name, PriceSingle = item.Price, PriceFull = item.Price, Amount = 1, ItemId = item.Id };

                    _context.CheckoutItemModels0.Add(checkoutItemModel);
                    await _context.SaveChangesAsync();
                }
               
            }
            
        }


    }
}
