using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCoME.Terminal.ViewModels
{
    [Export]
    public class MainViewModel
    {
        public IDisplayViewModel Display { get; }

        public ICardReaderViewModel CardReader { get; }

        public IBarcodeScannerViewModel BarcodeScanner { get; }

        public MainViewModel() { }

        [ImportingConstructor]
        public MainViewModel(IDisplayViewModel display, ICardReaderViewModel cardReader, IBarcodeScannerViewModel barcodeScanner)
        {
            Display = display;
            CardReader = cardReader;
            BarcodeScanner = barcodeScanner;
        }
    }
}
