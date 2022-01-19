using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCoME.Terminal.ViewModels
{
    public class CardDataEventArgs : EventArgs
    {
        public string CardId { get; }

        public CardDataEventArgs(string cardId)
        {
            CardId = cardId;
        }
    }
}
