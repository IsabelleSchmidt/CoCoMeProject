using System;
using System.Collections.Generic;
using System.Text;
using Tecan.Sila2;

namespace ContractsStore
{
    [SilaFeature]
    internal interface ICashDeskPc
    {
        Tuple<String, int> getItemById(Guid itemId); //return product and amount left

        void completeSale(Dictionary<String, int> sale);

    }
}