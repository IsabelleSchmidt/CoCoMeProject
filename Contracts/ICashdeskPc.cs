using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Tecan.Sila2;

namespace StoreContracts
{
    [SilaFeature]
    internal interface ICashdeskPc
    {
        Dictionary<Product, int> getItemById(Guid itemId);

        void completeSale(Dictionary<Product, int> sale);

    }
}
