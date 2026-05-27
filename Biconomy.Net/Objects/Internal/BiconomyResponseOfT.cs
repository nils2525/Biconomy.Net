using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Internal
{
    /// <summary>
    /// Biconomy REST response envelope with data.
    /// </summary>
    /// <typeparam name="T">Response data type.</typeparam>
    internal record BiconomyResponse<T> : BiconomyResponse
    {
        /// <summary>
        /// ["<c>data</c>"] Response payload.
        /// </summary>
        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }
}
