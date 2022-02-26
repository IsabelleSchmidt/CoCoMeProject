using Tecan.Sila2.Server;

var bootstrapper = Bootstrapper.Start(args);
Console.ReadLine();
bootstrapper.ShutdownServer();