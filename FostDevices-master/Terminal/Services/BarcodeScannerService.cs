using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using CoCoME.Terminal.Contracts;
using CoCoME.Terminal.ViewModels;

namespace CoCoME.Terminal.Services
{
    [Export(typeof(IBarcodeScannerService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class BarcodeScannerService : IBarcodeScannerService
    {
        private readonly IBarcodeScannerViewModel _viewModel;

        [ImportingConstructor]
        public BarcodeScannerService(IBarcodeScannerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public IObservable<string> ListenToBarcodes()
        {
            return Observable.FromEventPattern<BarcodeScannedEventArgs>(h => _viewModel.Scanned += h, h => _viewModel.Scanned -= h)
                .Select(scanEvent =>
                {
                    return scanEvent.EventArgs.Barcode;
                });
        }
    }
}
