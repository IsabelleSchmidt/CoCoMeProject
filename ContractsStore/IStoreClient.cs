using System;
using System.Collections.Generic;
using System.Text;
using Tecan.Sila2;

namespace ContractsStore
{
    [SilaFeature]
    internal interface IStoreClient
    {

        Dictionary<String, int> getProducts(); //Store Manager

        List<Guid> orderProducts(Dictionary<String, int> toBeOrdered); //Store Manager

        void rollInReceived(Guid orderId); //Stock Manager

        void changePrice(String product); //Store Manager



    }
}