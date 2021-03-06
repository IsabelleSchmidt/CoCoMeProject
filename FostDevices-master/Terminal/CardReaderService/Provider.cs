//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using CoCoME.Terminal.Contracts;
using System.Linq;
using Tecan.Sila2;
using Tecan.Sila2.Client;
using Tecan.Sila2.Server;

namespace CoCoME.Terminal.CardReaderService
{
    
    
    ///  <summary>
    /// A class that exposes the ICardReaderService interface via SiLA2
    /// </summary>
    [System.ComponentModel.Composition.ExportAttribute(typeof(IFeatureProvider))]
    [System.ComponentModel.Composition.PartCreationPolicyAttribute(System.ComponentModel.Composition.CreationPolicy.Shared)]
    public partial class CardReaderServiceProvider : IFeatureProvider
    {
        
        private ICardReaderService _implementation;
        
        private Tecan.Sila2.Server.ISiLAServer _server;
        
        private static Tecan.Sila2.Feature _feature = FeatureSerializer.LoadFromAssembly(typeof(CardReaderServiceProvider).Assembly, "CardReaderService.sila.xml");
        
        ///  <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="implementation">The implementation to exported through SiLA2</param>
        /// <param name="server">The SiLA2 server instance through which the implementation shall be exported</param>
        [System.ComponentModel.Composition.ImportingConstructorAttribute()]
        public CardReaderServiceProvider(ICardReaderService implementation, Tecan.Sila2.Server.ISiLAServer server)
        {
            _implementation = implementation;
            _server = server;
        }
        
        ///  <summary>
        /// The feature that is exposed by this feature provider
        /// </summary>
        /// <returns>A feature object</returns>
        public Tecan.Sila2.Feature FeatureDefinition
        {
            get
            {
                return _feature;
            }
        }
        
        ///  <summary>
        /// Registers the feature in the provided feature registration
        /// </summary>
        /// <param name="registration">The registration component to which the feature should be registered</param>
        public void Register(IServerBuilder registration)
        {
            registration.RegisterObservableCommand<AuthorizeRequestDto, AuthorizationData, AuthorizeResponseDto>("Authorize", Authorize, ConvertAuthorizeResponse, null);
            registration.RegisterUnobservableCommand<ConfirmRequestDto, EmptyRequest>("Confirm", Confirm);
            registration.RegisterUnobservableCommand<AbortRequestDto, EmptyRequest>("Abort", Abort);
        }
        
        private AuthorizeResponseDto ConvertAuthorizeResponse(AuthorizationData result)
        {
            return new AuthorizeResponseDto(result, _server);
        }
        
        ///  <summary>
        /// Executes the Authorize command
        /// </summary>
        /// <param name="request">A data transfer object that contains the command parameters</param>
        /// <returns>The command response wrapped in a data transfer object</returns>
        protected virtual Tecan.Sila2.IObservableCommand<AuthorizationData> Authorize(AuthorizeRequestDto request)
        {
            return new AuthorizeCommand(this, request);
        }
        
        ///  <summary>
        /// Executes the Confirm command
        /// </summary>
        /// <param name="request">A data transfer object that contains the command parameters</param>
        /// <returns>The command response wrapped in a data transfer object</returns>
        protected virtual EmptyRequest Confirm(ConfirmRequestDto request)
        {
            _implementation.Confirm();
            return EmptyRequest.Instance;
        }
        
        ///  <summary>
        /// Executes the Abort command
        /// </summary>
        /// <param name="request">A data transfer object that contains the command parameters</param>
        /// <returns>The command response wrapped in a data transfer object</returns>
        protected virtual EmptyRequest Abort(AbortRequestDto request)
        {
            try
            {
                _implementation.Abort(request.ErrorMessage.Extract(_server));
                return EmptyRequest.Instance;
            } catch (System.ArgumentException ex)
            {
                if ((ex.ParamName == "errorMessage"))
                {
                    throw _server.ErrorHandling.CreateValidationError("cocome.terminal/contracts/CardReaderService/v1/Command/Abort/Parameter/ErrorMessa" +
                            "ge", ex.Message);
                }
                throw _server.ErrorHandling.CreateUnknownValidationError(ex);
            }
        }
        
        ///  <summary>
        /// Gets the command with the given identifier
        /// </summary>
        /// <param name="commandIdentifier">A fully qualified command identifier</param>
        /// <returns>A method object or null, if the command is not supported</returns>
        public System.Reflection.MethodInfo GetCommand(string commandIdentifier)
        {
            if ((commandIdentifier == "cocome.terminal/contracts/CardReaderService/v1/Command/Authorize"))
            {
                return typeof(ICardReaderService).GetMethod("Authorize");
            }
            if ((commandIdentifier == "cocome.terminal/contracts/CardReaderService/v1/Command/Confirm"))
            {
                return typeof(ICardReaderService).GetMethod("Confirm");
            }
            if ((commandIdentifier == "cocome.terminal/contracts/CardReaderService/v1/Command/Abort"))
            {
                return typeof(ICardReaderService).GetMethod("Abort");
            }
            return null;
        }
        
        ///  <summary>
        /// Gets the property with the given identifier
        /// </summary>
        /// <param name="propertyIdentifier">A fully qualified property identifier</param>
        /// <returns>A property object or null, if the property is not supported</returns>
        public System.Reflection.PropertyInfo GetProperty(string propertyIdentifier)
        {
            return null;
        }
        
        private class AuthorizeCommand : Tecan.Sila2.ObservableCommand<CoCoME.Terminal.Contracts.AuthorizationData>
        {
            
            private CardReaderServiceProvider _parent;
            
            private AuthorizeRequestDto _request;
            
            public AuthorizeCommand(CardReaderServiceProvider parent, AuthorizeRequestDto request) : 
                    base(new System.Threading.CancellationTokenSource())
            {
                _parent = parent;
                _request = request;
            }
            
            public override System.Threading.Tasks.Task<CoCoME.Terminal.Contracts.AuthorizationData> Run()
            {
                return _parent._implementation.Authorize(((int)(_request.Amount.Extract(_parent._server))), _request.Challenge.ExtractToBytes(_parent._server), CancellationToken);
            }
        }
    }
}


