using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Tecan.Sila2;

namespace StoreContracts
{
    [SilaFeature]
    internal interface IStoreClient
    {

		Dictionary<Product, int> getProducts(); //Store Manager

		List<Guid> orderProducts(Dictionary<Product, int> toBeOrdered); //Store Manager

		void rollInReceived(Guid orderId); //Stock Manager

		void changePrice(Product product); //Store Manager

	}
}
