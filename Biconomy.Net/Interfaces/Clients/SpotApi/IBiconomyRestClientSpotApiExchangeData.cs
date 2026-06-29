using Biconomy.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Biconomy.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Biconomy spot exchange-data REST endpoints.
    /// </summary>
    public interface IBiconomyRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Get server time.
        /// <para><a href="https://github.com/BiconomyOfficial/APIDocs/blob/master/README_v3.md#server-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Server time.</returns>
        Task<HttpResult<BiconomyServerTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get exchange symbols.
        /// <para><a href="https://github.com/BiconomyOfficial/APIDocs/blob/master/README_v3.md#get-symbols" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Exchange symbols.</returns>
        Task<HttpResult<BiconomySymbol[]>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get all 24h tickers.
        /// <para><a href="https://github.com/BiconomyOfficial/APIDocs/blob/master/README_v3.md#get-tickers" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Exchange tickers.</returns>
        Task<HttpResult<BiconomyTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get order book depth.
        /// <para><a href="https://github.com/BiconomyOfficial/APIDocs/blob/master/README_v3.md#get-depth-information" /></para>
        /// </summary>
        /// <param name="symbol">Symbol in <c>BASE_QUOTE</c> format.</param>
        /// <param name="limit">Optional depth limit.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Order book depth.</returns>
        Task<HttpResult<BiconomyOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);
    }
}
