using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCoME.Terminal.ViewModels.Contracts
{
    public interface ICashboxViewModel
    {
        event EventHandler<CashboxButtonEventArgs> ButtonPressed;
    }
}
