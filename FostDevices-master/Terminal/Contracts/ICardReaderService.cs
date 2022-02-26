using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tecan.Sila2;

namespace CoCoME.Terminal.Contracts
{
    [SilaFeature]
    public interface ICardReaderService
    {
        Task<AuthorizationData> Authorize(int amount, byte[] challenge, CancellationToken cancellationToken);

        void Confirm();

        void Abort(string errorMessage);
    }
}
