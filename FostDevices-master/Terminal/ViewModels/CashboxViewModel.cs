using CoCoME.Terminal.Contracts;
using CoCoME.Terminal.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoCoME.Terminal.ViewModels
{
    [Export(typeof(ICashboxViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class CashboxViewModel : ViewModelBase, ICashboxViewModel
    {
        public ICommand PressButtonCommand { get; }

        public CashboxViewModel()
        {
            PressButtonCommand = new DelegateCommand(PressButton);
        }

        public event EventHandler<CashboxButtonEventArgs> ButtonPressed;

        private void PressButton(string button)
        {
            var parsedButton = (CashboxButton)Enum.Parse(typeof(CashboxButton), button);
            ButtonPressed?.Invoke(this, new CashboxButtonEventArgs(parsedButton));
        }
    }
}
