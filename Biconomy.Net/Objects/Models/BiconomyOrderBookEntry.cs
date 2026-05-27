using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models
{
    /// <summary>
    /// Biconomy order book entry.
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<BiconomyOrderBookEntry>))]
    public record BiconomyOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// ["<c>0</c>"] Price.
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }

        /// <summary>
        /// ["<c>1</c>"] Quantity.
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}
