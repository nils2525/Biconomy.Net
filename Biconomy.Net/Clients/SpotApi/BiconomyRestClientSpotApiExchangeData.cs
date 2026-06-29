using Biconomy.Net.Interfaces.Clients.SpotApi;
using Biconomy.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Biconomy.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BiconomyRestClientSpotApiExchangeData : IBiconomyRestClientSpotApiExchangeData
    {
        #region Statics
        private static readonly RequestDefinitionCache _definitions = new();
        #endregion

        #region Fields
        private readonly BiconomyRestClientSpotApi _baseClient;
        #endregion

        #region Constructors
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="baseClient">Base client.</param>
        public BiconomyRestClientSpotApiExchangeData(BiconomyRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        public async Task<HttpResult<BiconomyServerTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/time", BiconomyExchange.RateLimiter.Rest, 1, false);
            return await _baseClient.SendBiconomyAsync<BiconomyServerTime>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BiconomySymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/symbols", BiconomyExchange.RateLimiter.Rest, 1, false);
            return await _baseClient.SendBiconomyAsync<BiconomySymbol[]>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BiconomyTicker[]>> GetTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/tickers", BiconomyExchange.RateLimiter.Rest, 1, false);
            return await _baseClient.SendBiconomyAsync<BiconomyTicker[]>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BiconomyOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BiconomyExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/depth", BiconomyExchange.RateLimiter.Rest, 1, false);
            return await _baseClient.SendBiconomyAsync<BiconomyOrderBook>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion
    }
}
