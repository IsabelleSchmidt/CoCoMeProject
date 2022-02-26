using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tecan.Sila2;

namespace Terminal.Contracts
{
    [SilaFeature]
    public interface IPrintingService
    {
        void PrintLine(string line);

        void StartNext();
    }
}
