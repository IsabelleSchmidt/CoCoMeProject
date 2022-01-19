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
using TestConsole.DisplayController;

var connector = new ServerConnector(new DiscoveryExecutionManager());
var discovery = new ServerDiscovery(connector);
var executionManagerFactory = new ExecutionManagerFactory(Enumerable.Empty<IClientRequestInterceptor>());

var servers = discovery.GetServers(TimeSpan.FromSeconds(10), n => n.NetworkInterfaceType == NetworkInterfaceType.Loopback);

var terminalServer = servers.First(s => s.Info.Type == "Terminal");
var bankServer = servers.FirstOrDefault(s => s.Info.Type == "BankServer");

var terminalServerExecutionManager = executionManagerFactory.CreateExecutionManager(terminalServer);

var displayClient = new DisplayControllerClient(terminalServer.Channel, terminalServerExecutionManager);
var barcodeClient = new BarcodeScannerServiceClient(terminalServer.Channel, terminalServerExecutionManager);

var readBarcodes = barcodeClient.ListenToBarcodes();

Console.WriteLine("Scanning barcodes");
PrintNumberOfArticlesScanned(displayClient, readBarcodes);
Console.ReadLine();
readBarcodes.Cancel();
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

static async void PrintNumberOfArticlesScanned(IDisplayController displayController, IIntermediateObservableCommand<string> barcodes)
{
    var itemsScanned = 0;
    while (await barcodes.IntermediateValues.WaitToReadAsync())
    {
        if (barcodes.IntermediateValues.TryRead(out var barcode))
        {
            itemsScanned++;
            Console.WriteLine(barcode);
            displayController.SetDisplayText($"Scanned {barcode} ({itemsScanned} items total)");
        }
    }
}