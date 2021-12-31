using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Tecan.Sila2;

namespace Program
{
	[SilaFeature]
	internal interface IStoreServer
	{

		void queryItem(Item, Guid storeId);


	}
}
