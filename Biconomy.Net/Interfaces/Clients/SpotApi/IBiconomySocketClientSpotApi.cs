using Biconomy.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;

namespace Biconomy.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Biconomy spot socket API.
    /// </summary>
    public interface IBiconomySocketClientSpotApi
    {
        /// <summary>
        /// Subscribe to 24h state updates.
        /// <para><a href="https://github.com/BiconomyOfficial/APIDocs/blob/master/README_v3.md#24hour-market-state-stream" /></para>
        /// </summary>
        /// <param name="symbols">Symbols in <c>BASE_QUOTE</c> format.</param>
        /// <param name="onMessage">Update handler.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The subscription result.</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string[] symbols, Action<DataEvent<BiconomyStateUpdateEnvelope>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates.
        /// <para><a href="https://github.com/BiconomyOfficial/APIDocs/blob/master/README_v3.md#deal-stream" /></para>
        /// </summary>
        /// <param name="symbols">Symbols in <c>BASE_QUOTE</c> format.</param>
        /// <param name="onMessage">Update handler.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The subscription result.</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string[] symbols, Action<DataEvent<BiconomyDealsUpdate>> onMessage, CancellationToken ct = default);
    }
}
