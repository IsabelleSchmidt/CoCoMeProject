using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCoME.Terminal.ViewModels
{
    [Export(typeof(IDisplayViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DisplayViewModel : ViewModelBase, IDisplayViewModel
    {
        private string _display;

        public string Display
        {
            get => _display;
            set => Set(ref _display, value);
        }
    }
}
