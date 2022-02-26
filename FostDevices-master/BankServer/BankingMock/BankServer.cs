using BankServer.Contracts;
using Common.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.BankingMock
{
    [Export(typeof(IBankServer))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class BankServer : IBankServer
    {
        private readonly Random _random = new Random();
        private readonly ConcurrentQueue<PendingTransactionContext> _transactionQueue = new ConcurrentQueue<PendingTransactionContext>();
        private readonly ConcurrentDictionary<string, PendingTransactionContext> _transactionContexts = new ConcurrentDictionary<string, PendingTransactionContext>();
        private readonly ILog _log = LogManager.GetLogger<BankServer>();

        public void AuthorizePayment(string transactionContextId, string account, string authorizationToken)
        {
            _log.Info($"Requesting to authorize transaction {transactionContextId} from {account}");
            if (!_transactionContexts.TryGetValue(transactionContextId, out var pendingTransaction))
            {
                throw new UnknownTransactionException($"No transaction with id {transactionContextId} known.");
            }

            if (authorizationToken != CalculateAuthorizationToken(account, pendingTransaction))
            {
                throw new InvalidAuthorizationCodeException("The authorization token is invalid");
            }

            if (_random.NextDouble() < 0.05)
            {
                throw new InsufficientCreditException($"Authorization of {pendingTransaction.Amount / 100.0: C} failed: Not enough balance");
            }
            _log.Info($"Transaction {transactionContextId} is granted.");
        }
        private string CalculateAuthorizationToken(string account, TransactionContext transaction)
        {
            var hmac = new System.Security.Cryptography.HMACSHA256(transaction.Challenge);
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes($"{account}:{transaction.Amount}")));
        }

        public TransactionContext CreateContext(int amount)
        {
            DeleteExpiredTransactions();
            var contextId = Guid.NewGuid().ToString();
            _log.Info($"Create context {contextId} for {amount / 100.0: C}");
            var challenge = new byte[2048];
            _random.NextBytes(challenge);
            var context = new PendingTransactionContext(contextId, challenge, amount);
            _transactionContexts.TryAdd(contextId, context);
            _transactionQueue.Enqueue(context);
            return context;
        }

        private void DeleteExpiredTransactions()
        {
            while (_transactionQueue.TryPeek(out var nextTransactionToAbort) && nextTransactionToAbort.ValidUntil < DateTime.UtcNow)
            {
                if (_transactionQueue.TryDequeue(out var dequeued))
                {
                    if (dequeued == nextTransactionToAbort )
                    {
                        _transactionContexts.TryRemove(dequeued.ContextId, out _);
                    }
                    else
                    {
                        _transactionQueue.Enqueue(dequeued);
                    }
                }
            }
        }
    }
}
