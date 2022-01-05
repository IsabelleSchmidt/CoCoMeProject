using System;
using System.Collections.Generic;
using System.Text;
using Tecan.Sila2;

namespace ContractsStore
{
    [SilaFeature]
    internal interface IEnterpriseServer
    {
        Dictionary<String, int> triggerDataPushToEnterprise();

        void incomingDelivery(List<Dictionary<String, int>> deliveryInfo);

        void deliveryRequest(Dictionary<String, int> whatsNeeded);

    }
}