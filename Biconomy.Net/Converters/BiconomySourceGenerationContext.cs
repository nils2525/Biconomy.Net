using Biconomy.Net.Objects.Internal;
using Biconomy.Net.Objects.Models;
using Biconomy.Net.Objects.Models.Socket;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Biconomy.Net.Converters
{
    /// <summary>
    /// Source-generated JSON context for Biconomy.
    /// </summary>
    [JsonSerializable(typeof(BiconomyResponse<BiconomyServerTime>))]
    [JsonSerializable(typeof(BiconomyResponse<BiconomySymbol[]>))]
    [JsonSerializable(typeof(BiconomyResponse<BiconomyTicker[]>))]
    [JsonSerializable(typeof(BiconomyResponse<BiconomyOrderBook>))]
    [JsonSerializable(typeof(BiconomyServerTime))]
    [JsonSerializable(typeof(BiconomySymbol))]
    [JsonSerializable(typeof(BiconomySymbol[]))]
    [JsonSerializable(typeof(BiconomyTicker))]
    [JsonSerializable(typeof(BiconomyTicker[]))]
    [JsonSerializable(typeof(BiconomyOrderBook))]
    [JsonSerializable(typeof(BiconomyOrderBookEntry))]
    [JsonSerializable(typeof(BiconomyOrderBookEntry[]))]
    [JsonSerializable(typeof(BiconomyStateUpdate))]
    [JsonSerializable(typeof(BiconomyStateUpdateEnvelope))]
    [JsonSerializable(typeof(BiconomyDealEntry))]
    [JsonSerializable(typeof(BiconomyDealEntry[]))]
    [JsonSerializable(typeof(BiconomyDealsUpdate))]
    [JsonSerializable(typeof(BiconomySocketRequest))]
    [JsonSerializable(typeof(BiconomySocketResponse<BiconomySocketStatus>))]
    [JsonSerializable(typeof(BiconomySocketResponse<string>))]
    [JsonSerializable(typeof(BiconomySocketStatus))]
    [JsonSerializable(typeof(JsonElement))]
    [JsonSerializable(typeof(object[]))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(string[]))]
    [JsonSerializable(typeof(int))]
    [JsonSerializable(typeof(int?))]
    [JsonSerializable(typeof(long))]
    [JsonSerializable(typeof(long?))]
    [JsonSerializable(typeof(decimal))]
    [JsonSerializable(typeof(decimal?))]
    internal partial class BiconomySourceGenerationContext : JsonSerializerContext
    {
    }
}
