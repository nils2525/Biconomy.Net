using System.Text.Json;
using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models.Socket
{
    /// <summary>
    /// Biconomy socket response envelope.
    /// </summary>
    /// <typeparam name="T">Result payload type.</typeparam>
    public record BiconomySocketResponse<T>
    {
        /// <summary>
        /// ["<c>error</c>"] Error value.
        /// </summary>
        [JsonPropertyName("error")]
        public JsonElement? Error { get; set; }

        /// <summary>
        /// ["<c>result</c>"] Result payload.
        /// </summary>
        [JsonPropertyName("result")]
        public T? Result { get; set; }

        /// <summary>
        /// ["<c>id</c>"] Echoed request id.
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// Error message, if present.
        /// </summary>
        public string? ErrorMessage
        {
            get
            {
                if (Error is not { } error || error.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
                    return null;

                if (error.ValueKind == JsonValueKind.String)
                    return error.GetString();

                if (error.ValueKind == JsonValueKind.Object && error.TryGetProperty("message", out var message))
                    return message.GetString();

                return error.ToString();
            }
        }
    }
}
