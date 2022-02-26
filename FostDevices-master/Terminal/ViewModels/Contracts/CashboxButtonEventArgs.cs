using CoCoME.Terminal.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCoME.Terminal.ViewModels.Contracts
{
    public class CashboxButtonEventArgs : EventArgs
    {
        public CashboxButtonEventArgs(CashboxButton button)
        {
            Button = button;
        }

        public CashboxButton Button { get; }
    }
}
