using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Internal
{
    /// <summary>
    /// Biconomy REST response envelope.
    /// </summary>
    internal record BiconomyResponse
    {
        /// <summary>
        /// ["<c>code</c>"] Business response code.
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// ["<c>message</c>"] Response message.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
