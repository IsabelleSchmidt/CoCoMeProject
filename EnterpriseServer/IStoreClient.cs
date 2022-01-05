using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Tecan.Sila2;

namespace EnterpriseServer
{
    [SilaFeature]
    internal interface IStoreClient
    {

        Dictionary<Guid, int> generateDeliveryReport(Guid enterpriseId);


    }
}