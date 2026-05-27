using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models.Socket
{
    /// <summary>
    /// Biconomy socket request.
    /// </summary>
    internal record BiconomySocketRequest
    {
        /// <summary>
        /// ["<c>method</c>"] Request method.
        /// </summary>
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>params</c>"] Request parameters.
        /// </summary>
        [JsonPropertyName("params")]
        public object[] Params { get; set; } = [];

        /// <summary>
        /// ["<c>id</c>"] Request id.
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
