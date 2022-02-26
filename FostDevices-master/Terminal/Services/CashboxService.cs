using CoCoME.Terminal.Contracts;
using CoCoME.Terminal.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCoME.Terminal.Services
{
    [Export(typeof(ICashboxService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class CashboxService : ICashboxService
    {
        private readonly ICashboxViewModel _viewModel;

        [ImportingConstructor]
        public CashboxService(ICashboxViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public IObservable<CashboxButton> ListenToCashdeskButtons()
        {
            return Observable.FromEventPattern<CashboxButtonEventArgs>(h => _viewModel.ButtonPressed += h, h => _viewModel.ButtonPressed -= h)
                .Select(eventArgs => eventArgs.EventArgs.Button);
        }
    }
}
