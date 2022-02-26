using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCoME.Terminal.Contracts
{
    public class AuthorizationData
    {
        public AuthorizationData(int amount, string account, string authorizationToken)
        {
            Amount = amount;
            Account = account;
            AuthorizationToken = authorizationToken;
        }

        public int Amount { get; }

        public string Account { get; }

        public string AuthorizationToken { get; }
    }
}
