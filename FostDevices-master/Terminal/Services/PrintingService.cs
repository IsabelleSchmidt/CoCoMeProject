using CoCoME.Terminal.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Contracts;

namespace CoCoME.Terminal.Services
{
    [Export(typeof(IPrintingService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class PrintingService : IPrintingService
    {
        private readonly IPrinterViewModel _viewModel;
        private readonly Queue<string> _printQueue = new Queue<string>();

        [ImportingConstructor]
        public PrintingService(IPrinterViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void PrintLine(string line)
        {
            lock (_printQueue)
            {
                _printQueue.Enqueue(line);
                while (_printQueue.Count > 30)
                {
                    _printQueue.Dequeue();
                }
                _viewModel.Print = string.Join(Environment.NewLine, _printQueue);
            }
        }

        public void StartNext()
        {
            PrintLine(string.Empty);
            PrintLine("----------------");
            PrintLine(string.Empty);
        }
    }
}
