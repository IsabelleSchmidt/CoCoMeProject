using KassenSystem.Data;
using KassenSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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


namespace KassenSystem.Controllers
{
    public class CashRegisterSystemController : Controller
    {

        private readonly ItemContext _context;
        private readonly ILogger<CashRegisterSystemController> _logger;
        private List<string> _items;

        public CashRegisterSystemController(ILogger<CashRegisterSystemController> logger, ItemContext context)
        {
            _logger = logger;
            _context = context;
            _items = new List<string>();

        }
     

        public async Task<IActionResult> Index()
        {
            GetItemsListener();
            float pricesum = _context.CheckoutItemModels0.Sum(p => p.PriceFull);
            ViewData["PriceSum"] = pricesum / 100;
            return View(await _context.CheckoutItemModels0.ToListAsync());
        }

        public async Task<IActionResult> FinishScanning()
        {
            //StopItemsListener();
            float pricesum = _context.CheckoutItemModels0.Sum(p => p.PriceFull);
            ViewData["PriceSum"] = pricesum / 100;
            return View(await _context.CheckoutItemModels0.ToListAsync());
        }


        public void GetItemsListener()
        {
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
            //var cashboxButtons = cashboxClient.ListenToCashdeskButtons();
            //Console.WriteLine("Reading cashbox buttons");
            //Console.WriteLine("Press some of the cashbox buttons and see how the display adjust. Press Enter to continue");
            //var listenToCashBoxButtons = cashboxClient.ListenToCashdeskButtons();
            //DisplayButtonsPressed(displayClient, listenToCashBoxButtons);
            //Console.ReadLine();
            //listenToCashBoxButtons.Cancel();

            // demo of printer and barcode reader
            var printerClient = new PrintingServiceClient(terminalServer.Channel, terminalServerExecutionManager);
            var barcodeClient = new BarcodeScannerServiceClient(terminalServer.Channel, terminalServerExecutionManager);
            var readBarcodes = barcodeClient.ListenToBarcodes();
            Console.WriteLine("Scanning barcodes");
            Console.WriteLine("Select some items in the terminal application and see the printer adjusting, press Enter to continue");
            PrintNumberOfArticlesScanned(printerClient, readBarcodes);
            //Console.ReadLine();
            //readBarcodes.Cancel();
            //Console.WriteLine("Cancel");
        }
        private async void PrintNumberOfArticlesScanned(IPrintingService printer, IIntermediateObservableCommand<string> barcodes)
        {
            var itemsScanned = 0;
            Console.WriteLine("waiting");
            while (await barcodes.IntermediateValues.WaitToReadAsync())
            {
            //await barcodes.IntermediateValues.WaitToReadAsync();
                if (barcodes.IntermediateValues.TryRead(out var barcode))
                {
                    itemsScanned++;
                    printer.PrintLine(barcode);
                    Console.WriteLine($"Scanned {barcode} ({itemsScanned} items total)");
                    SaveArticle("lala");
                    //barcodes.Cancel();

                }
            Console.WriteLine("...not");
            }
        }
        

        private async void SaveArticle(string barcode)
        {
            _items.Add(barcode);
            //_context.CheckoutItemModels.Add(new CheckoutItemModel() { Amount = 1, Name = barcode, PriceFull = 100, PriceSingle = 100 });
            //await _context.SaveChangesAsync();
            //barcodes.Cancel();

               
        }


      
    }
}
