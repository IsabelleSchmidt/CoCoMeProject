using System;

namespace CoCoME.Terminal.ViewModels
{
    public interface IBarcodeScannerViewModel
    {
        event EventHandler<BarcodeScannedEventArgs> Scanned;
    }
}