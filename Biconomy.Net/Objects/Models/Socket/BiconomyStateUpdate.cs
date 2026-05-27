using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models.Socket
{
    /// <summary>
    /// Biconomy 24h state update.
    /// </summary>
    public record BiconomyStateUpdate
    {
        /// <summary>
        /// ["<c>period</c>"] Rolling window length in seconds.
        /// </summary>
        [JsonPropertyName("period")]
        public int Period { get; set; }

        /// <summary>
        /// ["<c>last</c>"] Last trade price.
        /// </summary>
        [JsonPropertyName("last")]
        public decimal Last { get; set; }

        /// <summary>
        /// ["<c>open</c>"] Open price.
        /// </summary>
        [JsonPropertyName("open")]
        public decimal Open { get; set; }

        /// <summary>
        /// ["<c>close</c>"] Close price.
        /// </summary>
        [JsonPropertyName("close")]
        public decimal Close { get; set; }

        /// <summary>
        /// ["<c>high</c>"] High price.
        /// </summary>
        [JsonPropertyName("high")]
        public decimal High { get; set; }

        /// <summary>
        /// ["<c>low</c>"] Low price.
        /// </summary>
        [JsonPropertyName("low")]
        public decimal Low { get; set; }

        /// <summary>
        /// ["<c>volume</c>"] Base volume.
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }

        /// <summary>
        /// ["<c>deal</c>"] Quote volume.
        /// </summary>
        [JsonPropertyName("deal")]
        public decimal Deal { get; set; }
    }
}
