using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Contracts
{
    public class UnknownTransactionException : Exception
    {
        public UnknownTransactionException(string? message) : base(message)
        {
        }
    }
}
