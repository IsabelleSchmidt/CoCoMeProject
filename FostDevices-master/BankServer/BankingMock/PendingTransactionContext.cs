using BankServer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.BankingMock
{
    internal class PendingTransactionContext : TransactionContext
    {
        public DateTime ValidUntil { get; }

        public PendingTransactionContext(string contextId, byte[] challenge, int amount) : base(contextId, challenge, amount)
        {
            ValidUntil = DateTime.UtcNow.AddMinutes(1);
        }
    }
}
