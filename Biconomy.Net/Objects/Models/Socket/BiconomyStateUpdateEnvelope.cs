using Biconomy.Net.Converters;
using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models.Socket
{
    /// <summary>
    /// Biconomy <c>state.update</c> push envelope.
    /// </summary>
    [JsonConverter(typeof(BiconomyStateUpdateConverter))]
    public record BiconomyStateUpdateEnvelope
    {
        /// <summary>
        /// Symbol the state applies to.
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// State update data.
        /// </summary>
        public BiconomyStateUpdate Data { get; set; } = new();
    }
}
