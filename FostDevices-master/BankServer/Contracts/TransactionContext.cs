using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tecan.Sila2;

namespace BankServer.Contracts
{
    public class TransactionContext
    {
        public string ContextId { get; }

        public byte[] Challenge { get; }

        [Unit("€", Factor = 0.01, Mole = 1)]
        public int Amount { get; }

        public TransactionContext(string contextId, byte[] challenge, int amount)
        {
            ContextId = contextId;
            Challenge = challenge;
            Amount = amount;
        }
    }
}
