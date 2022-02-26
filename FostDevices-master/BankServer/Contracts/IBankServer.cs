using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tecan.Sila2;

namespace BankServer.Contracts
{
    [SilaFeature]
    public interface IBankServer
    {
        TransactionContext CreateContext(int amount);

        void AuthorizePayment(string transactionContextId, string account, string authorizationToken);
    }
}
