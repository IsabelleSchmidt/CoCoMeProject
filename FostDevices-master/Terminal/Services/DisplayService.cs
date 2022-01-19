using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using CoCoME.Terminal.Contracts;
using CoCoME.Terminal.ViewModels;

namespace CoCoME.Terminal.Services
{
    [Export(typeof(IDisplayController))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class DisplayService : IDisplayController
    {
        private readonly IDisplayViewModel _displayModel;

        [ImportingConstructor]
        public DisplayService(IDisplayViewModel displayModel)
        {
            _displayModel = displayModel;
        }

        public void SetDisplayText(string displayText)
        {
            _displayModel.Display = displayText;
        }
    }
}
