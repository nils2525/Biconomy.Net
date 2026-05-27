using Biconomy.Net.Objects.Models.Socket;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Biconomy.Net.Converters
{
    /// <summary>
    /// Converts Biconomy <c>state.update</c> envelopes.
    /// </summary>
    internal class BiconomyStateUpdateConverter : JsonConverter<BiconomyStateUpdateEnvelope>
    {
        #region Methods
        /// <inheritdoc />
        public override BiconomyStateUpdateEnvelope Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);
            var parameters = document.RootElement.GetProperty("params");
            return new BiconomyStateUpdateEnvelope
            {
                Symbol = parameters[0].GetString() ?? string.Empty,
                Data = parameters[1].Deserialize<BiconomyStateUpdate>(options) ?? new BiconomyStateUpdate()
            };
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, BiconomyStateUpdateEnvelope value, JsonSerializerOptions options)
            => throw new NotSupportedException();
        #endregion
    }
}
