using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoCoME.Terminal.ViewModels
{
    [Export(typeof(IBarcodeScannerViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class BarcodeScannerViewModel : ViewModelBase, IBarcodeScannerViewModel
    {
        private string _barcode;

        public string Barcode
        {
            get => _barcode;
            set => Set(ref _barcode, value);
        }

        public ICommand ScanCommand { get; }

        public ICommand ScanChocolateCommand { get; }

        public BarcodeScannerViewModel()
        {
            ScanCommand = new DelegateCommand(Scan);
        }

        private void Scan(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                Scanned?.Invoke(this, new BarcodeScannedEventArgs(Barcode));
            }
            else
            {
                Scanned?.Invoke(this, new BarcodeScannedEventArgs(barcode));
            }
        }

        public event EventHandler<BarcodeScannedEventArgs> Scanned;
    }
}
