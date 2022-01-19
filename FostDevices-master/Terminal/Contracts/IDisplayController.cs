using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tecan.Sila2;

namespace CoCoME.Terminal.Contracts
{
    [SilaFeature]
    public interface IDisplayController
    {
        void SetDisplayText(string displayText);
    }
}
