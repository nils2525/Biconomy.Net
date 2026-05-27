using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models.Socket
{
    /// <summary>
    /// Biconomy socket status payload.
    /// </summary>
    public record BiconomySocketStatus
    {
        /// <summary>
        /// ["<c>status</c>"] Status value.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}
