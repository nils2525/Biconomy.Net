using CryptoExchange.Net.Interfaces.Clients;

namespace Biconomy.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Biconomy spot REST API.
    /// </summary>
    public interface IBiconomyRestClientSpotApi : IRestApiClient
    {
        /// <summary>
        /// Exchange-data endpoints.
        /// </summary>
        IBiconomyRestClientSpotApiExchangeData ExchangeData { get; }
    }
}
