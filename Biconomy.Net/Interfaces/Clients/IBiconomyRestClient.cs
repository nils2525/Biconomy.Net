using Biconomy.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Interfaces.Clients;

namespace Biconomy.Net.Interfaces.Clients
{
    /// <summary>
    /// Biconomy REST client.
    /// </summary>
    public interface IBiconomyRestClient : IRestClient
    {
        /// <summary>
        /// Spot API client.
        /// </summary>
        IBiconomyRestClientSpotApi SpotApi { get; }
    }
}
