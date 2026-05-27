using Biconomy.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Interfaces.Clients;

namespace Biconomy.Net.Interfaces.Clients
{
    /// <summary>
    /// Biconomy socket client.
    /// </summary>
    public interface IBiconomySocketClient : ISocketClient
    {
        /// <summary>
        /// Spot API client.
        /// </summary>
        IBiconomySocketClientSpotApi SpotApi { get; }
    }
}
