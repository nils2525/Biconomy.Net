using Biconomy.Net.Clients.SpotApi;
using Biconomy.Net.Interfaces.Clients;
using Biconomy.Net.Interfaces.Clients.SpotApi;
using Biconomy.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Biconomy.Net.Clients
{
    /// <inheritdoc cref="IBiconomySocketClient" />
    public class BiconomySocketClient : BaseSocketClient<BiconomyEnvironment, BiconomyCredentials>, IBiconomySocketClient
    {
        #region Properties
        /// <inheritdoc />
        public IBiconomySocketClientSpotApi SpotApi { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of the Biconomy socket client.
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate.</param>
        public BiconomySocketClient(Action<BiconomySocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        { }

        /// <summary>
        /// Create a new instance of the Biconomy socket client.
        /// </summary>
        /// <param name="options">Options.</param>
        /// <param name="loggerFactory">Logger factory.</param>
        public BiconomySocketClient(IOptions<BiconomySocketOptions> options, ILoggerFactory? loggerFactory = null)
            : base(loggerFactory, BiconomyExchange.ExchangeName)
        {
            Initialize(options.Value);
            SpotApi = AddApiClient(new BiconomySocketClientSpotApi(loggerFactory, options.Value));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set the default options to be used when creating new clients.
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate.</param>
        public static void SetDefaultOptions(Action<BiconomySocketOptions> optionsDelegate)
        {
            BiconomySocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }
        #endregion
    }
}
