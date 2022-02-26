using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCoME.Terminal.ViewModels
{
    public class BarcodeScannedEventArgs : EventArgs
    {
        public string Barcode { get; }

        public BarcodeScannedEventArgs(string barcode)
        {
            Barcode = barcode;
        }
    }
}
