using Biconomy.Net.Clients.MessageHandlers;
using Biconomy.Net.Interfaces.Clients.SpotApi;
using Biconomy.Net.Objects.Internal;
using Biconomy.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;

namespace Biconomy.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IBiconomyRestClientSpotApi" />
    internal class BiconomyRestClientSpotApi : RestApiClient<BiconomyEnvironment, BiconomyAuthenticationProvider, BiconomyCredentials>, IBiconomyRestClientSpotApi
    {
        #region Properties
        /// <summary>
        /// Strongly-typed client options.
        /// </summary>
        public new BiconomyRestOptions ClientOptions => (BiconomyRestOptions)base.ClientOptions;

        /// <inheritdoc />
        protected override ErrorMapping ErrorMapping => BiconomyErrors.SpotRestErrors;

        /// <inheritdoc />
        protected override IRestMessageHandler MessageHandler { get; } = new BiconomyRestMessageHandler(BiconomyErrors.SpotRestErrors);

        /// <inheritdoc />
        public IBiconomyRestClientSpotApiExchangeData ExchangeData { get; }

        /// <inheritdoc />
        public string ExchangeName => BiconomyExchange.ExchangeName;
        #endregion

        #region Constructors
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="baseClient">Base client.</param>
        /// <param name="loggerFactory">Logger factory.</param>
        /// <param name="httpClient">HTTP client.</param>
        /// <param name="options">Options.</param>
        internal BiconomyRestClientSpotApi(BiconomyRestClient baseClient, ILoggerFactory? loggerFactory, HttpClient? httpClient, BiconomyRestOptions options)
            : base(loggerFactory, BiconomyExchange.ExchangeName, httpClient, options.Environment.RestClientAddress, options, options.SpotOptions)
        {
            ExchangeData = new BiconomyRestClientSpotApiExchangeData(this);
            StandardRequestHeaders = new Dictionary<string, string>
            {
                { "User-Agent", "CryptoExchange.Net/" + baseClient.CryptoExchangeLibVersion }
            };
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer()
            => new SystemTextJsonMessageSerializer(BiconomyExchange._serializerContext);

        /// <inheritdoc />
        protected override BiconomyAuthenticationProvider CreateAuthenticationProvider(BiconomyCredentials credentials)
            => new(credentials);

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => Task.FromResult(new HttpResult<DateTime>(BiconomyExchange.ExchangeName, DateTime.UtcNow, null));

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => BiconomyExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        internal async Task<HttpResult<T>> SendBiconomyAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<BiconomyResponse<T>>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<T>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }
        #endregion
    }
}
