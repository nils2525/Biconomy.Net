using Biconomy.Net.Enums;
using System.Text.Json.Serialization;

namespace Biconomy.Net.Objects.Models
{
    /// <summary>
    /// Biconomy symbol metadata.
    /// </summary>
    public record BiconomySymbol
    {
        /// <summary>
        /// ["<c>symbol</c>"] Trading pair.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>baseAsset</c>"] Base asset.
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>quoteAsset</c>"] Quote asset.
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>pricePrecision</c>"] Price precision.
        /// </summary>
        [JsonPropertyName("pricePrecision")]
        public int PricePrecision { get; set; }

        /// <summary>
        /// ["<c>quantityPrecision</c>"] Quantity precision.
        /// </summary>
        [JsonPropertyName("quantityPrecision")]
        public int QuantityPrecision { get; set; }

        /// <summary>
        /// ["<c>status</c>"] Symbol status.
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }

        /// <summary>
        /// ["<c>tickSize</c>"] Minimum price step.
        /// </summary>
        [JsonPropertyName("tickSize")]
        public decimal TickSize { get; set; }

        /// <summary>
        /// ["<c>minQuantity</c>"] Minimum order quantity.
        /// </summary>
        [JsonPropertyName("minQuantity")]
        public decimal MinQuantity { get; set; }

        /// <summary>
        /// ["<c>minQuantityBase</c>"] Minimum base-asset quantity.
        /// </summary>
        [JsonPropertyName("minQuantityBase")]
        public decimal MinQuantityBase { get; set; }

        /// <summary>
        /// ["<c>minQuantityQuote</c>"] Minimum quote-asset quantity.
        /// </summary>
        [JsonPropertyName("minQuantityQuote")]
        public decimal MinQuantityQuote { get; set; }

        /// <summary>
        /// ["<c>limitTakerFee</c>"] Limit-order taker fee.
        /// </summary>
        [JsonPropertyName("limitTakerFee")]
        public decimal LimitTakerFee { get; set; }

        /// <summary>
        /// ["<c>limitMakerFee</c>"] Limit-order maker fee.
        /// </summary>
        [JsonPropertyName("limitMakerFee")]
        public decimal LimitMakerFee { get; set; }

        /// <summary>
        /// ["<c>marketTakerFee</c>"] Market-order taker fee.
        /// </summary>
        [JsonPropertyName("marketTakerFee")]
        public decimal MarketTakerFee { get; set; }
    }
}
