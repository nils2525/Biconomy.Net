using Biconomy.Net.Converters;
using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models.Socket
{
    /// <summary>
    /// Biconomy <c>deals.update</c> push envelope.
    /// </summary>
    [JsonConverter(typeof(BiconomyDealsUpdateConverter))]
    public record BiconomyDealsUpdate
    {
        /// <summary>
        /// Symbol the deals apply to.
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Deal entries.
        /// </summary>
        public BiconomyDealEntry[] Deals { get; set; } = [];
    }
}
