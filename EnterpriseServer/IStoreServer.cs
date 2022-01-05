using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Tecan.Sila2;

namespace EnterpriseServer
{
    [SilaFeature]
    internal interface IStoreServer
    {
        void queryItem(List<Product> product, Guid storeId);

    }
}