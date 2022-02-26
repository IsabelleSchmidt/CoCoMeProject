using CoCoME.Terminal.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCoME.Terminal.ViewModels
{
    [Export(typeof(IPrinterViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class PrinterViewModel : ViewModelBase, IPrinterViewModel
    {
        private string _print;

        public string Print { get => _print; set => Set(ref _print, value); }
    }
}
