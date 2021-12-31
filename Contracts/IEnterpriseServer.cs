using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Tecan.Sila2;

namespace StoreContracts
{
    [SilaFeature]
    internal interface IEnterpriseServer
    {
        void triggerDataPushToEnterprise();

        List<Product> incomingDelivery(List<Product>, int storeamount);

        void deliveryRequest(List<Product>, int amount)

    }
}
