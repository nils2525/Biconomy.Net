using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Biconomy.Net.Enums
{
    /// <summary>
    /// Taker side for a Biconomy deal.
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DealSide>))]
    public enum DealSide
    {
        /// <summary>
        /// ["<c>buy</c>", "<c>BUY</c>"] Buy.
        /// </summary>
        [Map("buy", "BUY")]
        Buy,

        /// <summary>
        /// ["<c>sell</c>", "<c>SELL</c>"] Sell.
        /// </summary>
        [Map("sell", "SELL")]
        Sell
    }
}
