using Biconomy.Net.Clients.SpotApi;
using Biconomy.Net.Interfaces.Clients;
using Biconomy.Net.Interfaces.Clients.SpotApi;
using Biconomy.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Biconomy.Net.Clients
{
    /// <inheritdoc cref="IBiconomyRestClient" />
    public class BiconomyRestClient : BaseRestClient<BiconomyEnvironment, BiconomyCredentials>, IBiconomyRestClient
    {
        #region Properties
        /// <inheritdoc />
        public IBiconomyRestClientSpotApi SpotApi { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of the Biconomy REST client.
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate.</param>
        public BiconomyRestClient(Action<BiconomyRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        { }

        /// <summary>
        /// Create a new instance of the Biconomy REST client.
        /// </summary>
        /// <param name="httpClient">HTTP client.</param>
        /// <param name="loggerFactory">Logger factory.</param>
        /// <param name="options">Options.</param>
        public BiconomyRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<BiconomyRestOptions> options)
            : base(loggerFactory, BiconomyExchange.ExchangeName)
        {
            Initialize(options.Value);
            SpotApi = AddApiClient(new BiconomyRestClientSpotApi(this, _logger, httpClient, options.Value));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set the default options to be used when creating new clients.
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate.</param>
        public static void SetDefaultOptions(Action<BiconomyRestOptions> optionsDelegate)
        {
            BiconomyRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }
        #endregion
    }
}
