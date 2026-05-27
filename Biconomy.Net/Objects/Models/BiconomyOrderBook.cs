using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models
{
    /// <summary>
    /// Biconomy order book.
    /// </summary>
    public record BiconomyOrderBook
    {
        /// <summary>
        /// ["<c>asks</c>"] Ask levels.
        /// </summary>
        [JsonPropertyName("asks")]
        public BiconomyOrderBookEntry[] Asks { get; set; } = [];

        /// <summary>
        /// ["<c>bids</c>"] Bid levels.
        /// </summary>
        [JsonPropertyName("bids")]
        public BiconomyOrderBookEntry[] Bids { get; set; } = [];
    }
}
