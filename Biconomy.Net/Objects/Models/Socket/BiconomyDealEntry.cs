using Biconomy.Net.Enums;
using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models.Socket
{
    /// <summary>
    /// Biconomy deal entry.
    /// </summary>
    public record BiconomyDealEntry
    {
        /// <summary>
        /// ["<c>amount</c>"] Base-asset quantity.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// ["<c>time</c>"] Trade timestamp in seconds with fractional precision.
        /// </summary>
        [JsonPropertyName("time")]
        public decimal Time { get; set; }

        /// <summary>
        /// ["<c>id</c>"] Trade id.
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// ["<c>type</c>"] Taker side.
        /// </summary>
        [JsonPropertyName("type")]
        public DealSide Type { get; set; }

        /// <summary>
        /// ["<c>price</c>"] Trade price.
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Convert the wire timestamp to UTC.
        /// </summary>
        /// <returns>UTC timestamp.</returns>
        public DateTime GetTimestamp()
            => DateTimeOffset.FromUnixTimeMilliseconds((long)(Time * 1000m)).UtcDateTime;
    }
}
