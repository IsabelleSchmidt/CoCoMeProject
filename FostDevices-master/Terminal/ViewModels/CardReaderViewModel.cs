using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoCoME.Terminal.ViewModels
{
    [Export(typeof(ICardReaderViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class CardReaderViewModel : ViewModelBase, ICardReaderViewModel
    {
        private string _amount;

        public string Display
        {
            get => _amount;
            set => Set(ref _amount, value);
        }

        public ICommand AuthorizeCommand { get; }

        public ICommand CancelCommand { get; }

        public CardReaderViewModel()
        {
            AuthorizeCommand = new DelegateCommand(Authorize);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Authorize(string cardId)
        {
            TransactionAuthorized?.Invoke(this, new CardDataEventArgs(cardId));
        }

        private void Cancel(string parameter)
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Cancelled;

        public event EventHandler<CardDataEventArgs> TransactionAuthorized;
    }
}
