using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models
{
    /// <summary>
    /// Biconomy 24h ticker.
    /// </summary>
    public record BiconomyTicker
    {
        /// <summary>
        /// ["<c>symbol</c>"] Trading pair.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>last</c>"] Last price.
        /// </summary>
        [JsonPropertyName("last")]
        public decimal Last { get; set; }

        /// <summary>
        /// ["<c>high</c>"] 24h high.
        /// </summary>
        [JsonPropertyName("high")]
        public decimal High { get; set; }

        /// <summary>
        /// ["<c>low</c>"] 24h low.
        /// </summary>
        [JsonPropertyName("low")]
        public decimal Low { get; set; }

        /// <summary>
        /// ["<c>vol</c>"] 24h base volume.
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Volume { get; set; }

        /// <summary>
        /// ["<c>deal</c>"] 24h quote volume.
        /// </summary>
        [JsonPropertyName("deal")]
        public decimal Deal { get; set; }

        /// <summary>
        /// ["<c>change</c>"] 24h price-change ratio.
        /// </summary>
        [JsonPropertyName("change")]
        public decimal Change { get; set; }

        /// <summary>
        /// ["<c>buy</c>"] Best bid price.
        /// </summary>
        [JsonPropertyName("buy")]
        public decimal Buy { get; set; }

        /// <summary>
        /// ["<c>sell</c>"] Best ask price.
        /// </summary>
        [JsonPropertyName("sell")]
        public decimal Sell { get; set; }
    }
}
