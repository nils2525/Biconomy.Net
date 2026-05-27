using Biconomy.Net.Objects.Models.Socket;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Biconomy.Net.Converters
{
    /// <summary>
    /// Converts Biconomy <c>deals.update</c> envelopes.
    /// </summary>
    internal class BiconomyDealsUpdateConverter : JsonConverter<BiconomyDealsUpdate>
    {
        #region Methods
        /// <inheritdoc />
        public override BiconomyDealsUpdate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);
            var parameters = document.RootElement.GetProperty("params");
            return new BiconomyDealsUpdate
            {
                Symbol = parameters[0].GetString() ?? string.Empty,
                Deals = parameters[1].Deserialize<BiconomyDealEntry[]>(options) ?? []
            };
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, BiconomyDealsUpdate value, JsonSerializerOptions options)
            => throw new NotSupportedException();
        #endregion
    }
}
