// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.NetworkInformation;
using Tecan.Sila2;
using Tecan.Sila2.Client;
using Tecan.Sila2.Client.ExecutionManagement;
using Tecan.Sila2.Discovery;
using TestConsole;
using TestConsole.BankServer;
using TestConsole.BarcodeScannerService;
using TestConsole.CardReaderService;
using TestConsole.CashboxService;
using TestConsole.DisplayController;
using TestConsole.PrintingService;

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
Console.ReadLine();
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

// demo of bank transactions
Console.WriteLine("The following is an example of a bank transaction");
var cardReader = new CardReaderServiceClient(terminalServer.Channel, terminalServerExecutionManager);
var bankInterface = new BankServerClient(bankServer.Channel, executionManagerFactory.CreateExecutionManager(bankServer));

var context = bankInterface.CreateContext(5000);
var authorizeCommand = cardReader.Authorize(5000, context.Challenge);
var token = await authorizeCommand.Response;
try
{
    bankInterface.AuthorizePayment(context.ContextId, token.Account, token.AuthorizationToken);
    cardReader.Confirm();
}
catch (Exception ex)
{
    cardReader.Abort(ex.Message);
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