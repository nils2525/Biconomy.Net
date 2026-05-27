using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models
{
    /// <summary>
    /// Biconomy server time.
    /// </summary>
    public record BiconomyServerTime
    {
        /// <summary>
        /// ["<c>serverTime</c>"] Server time in milliseconds.
        /// </summary>
        [JsonPropertyName("serverTime")]
        public long ServerTime { get; set; }

        /// <summary>
        /// Server time as UTC <see cref="DateTime"/>.
        /// </summary>
        public DateTime ServerTimeUtc => DateTimeOffset.FromUnixTimeMilliseconds(ServerTime).UtcDateTime;
    }
}
