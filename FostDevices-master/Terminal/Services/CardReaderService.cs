using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using CoCoME.Terminal.Contracts;
using CoCoME.Terminal.ViewModels;

namespace CoCoME.Terminal.Services
{
    [Export(typeof(ICardReaderService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class CardReaderService : ICardReaderService
    {
        private TaskCompletionSource<AuthorizationData> _currentTransaction;
        private int _amount;
        private byte[] _challenge;
        private readonly ICardReaderViewModel _viewModel;

        [ImportingConstructor]
        public CardReaderService(ICardReaderViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.TransactionAuthorized += TransactionAuthorized;
            _viewModel.Cancelled += TransactionCancelled;
        }

        private void TransactionCancelled(object sender, EventArgs e)
        {
            CancelTransaction();
        }

        private void TransactionAuthorized(object sender, CardDataEventArgs e)
        {
            var transaction = Interlocked.Exchange(ref _currentTransaction, null);
            if (transaction != null)
            {
                transaction.TrySetResult(new AuthorizationData(_amount, e.CardId, CalculateAuthorizationToken(e.CardId)));
                _viewModel.Display = "Uploading Data";
            }
        }

        private string CalculateAuthorizationToken(string cardId)
        {
            var hmac = new System.Security.Cryptography.HMACSHA256(_challenge);
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes($"{cardId}:{_amount}")));
        }

        private void CancelTransaction()
        {
            var transaction = Interlocked.Exchange(ref _currentTransaction, null);
            if (transaction != null)
            {
                transaction.SetCanceled();
                _viewModel.Display = "Operation cancelled";
            }
        }

        public Task<AuthorizationData> Authorize(int amount, byte[] challenge, CancellationToken cancellationToken)
        {
            var transaction = new TaskCompletionSource<AuthorizationData>();
            if (Interlocked.CompareExchange(ref _currentTransaction, transaction, null) != null)
            {
                throw new InvalidOperationException("Another transaction is currently in progress.");
            }

            _amount = amount;
            _challenge = challenge;

            _viewModel.Display = (amount / 100.0).ToString("C");
            cancellationToken.Register(CancelTransaction);

            return transaction.Task;
        }

        public void Confirm()
        {
            _viewModel.Display = "Transaction confirmed";
        }

        public void Abort(string errorMessage)
        {
            _viewModel.Display = errorMessage;
        }
    }
}
