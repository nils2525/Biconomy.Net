using Biconomy.Net.Converters;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.SharedApis;
using System.Text.Json;

namespace Biconomy.Net
{
    /// <summary>
    /// Biconomy exchange information and configuration.
    /// Official docs: <see href="https://github.com/BiconomyOfficial/APIDocs/blob/master/README_v3.md" />.
    /// </summary>
    public static class BiconomyExchange
    {
        #region Statics
        internal static JsonSerializerOptions _serializerContext = SerializerOptions.WithConverters(JsonSerializerContextCache.GetOrCreate<BiconomySourceGenerationContext>());
        internal static readonly ParameterSerializationSettings _parameterSerializationSettings = new()
        {
            Decimal = DecimalSerialization.String,
            Array = ArrayParametersSerialization.MultipleValues,
            Sort = false
        };
        #endregion

        #region Properties
        /// <summary>
        /// Platform metadata.
        /// </summary>
        public static PlatformInfo Metadata { get; } = new(
            "Biconomy",
            "Biconomy",
            string.Empty,
            "https://www.biconomy.com/",
            ["https://github.com/BiconomyOfficial/APIDocs/blob/master/README_v3.md"],
            PlatformType.CryptoCurrencyExchange,
            CentralizationType.Centralized,
            BiconomyEnvironment.All);

        /// <summary>
        /// Exchange name.
        /// </summary>
        public static string ExchangeName => "Biconomy";

        /// <summary>
        /// Display exchange name.
        /// </summary>
        public static string DisplayName => "Biconomy";

        /// <summary>
        /// Url to the main website.
        /// </summary>
        public static string Url => "https://www.biconomy.com/";

        /// <summary>
        /// Urls to the API documentation.
        /// </summary>
        public static string[] ApiDocsUrl { get; } = ["https://github.com/BiconomyOfficial/APIDocs/blob/master/README_v3.md"];

        /// <summary>
        /// Type of exchange.
        /// </summary>
        public static ExchangeType Type => ExchangeType.CEX;

        /// <summary>
        /// Aliases for Biconomy assets.
        /// </summary>
        public static AssetAliasConfiguration AssetAliases { get; } = new()
        {
            Aliases = []
        };

        /// <summary>
        /// Rate limiter configuration for the Biconomy API.
        /// </summary>
        public static BiconomyRateLimiters RateLimiter { get; } = new();
        #endregion

        #region Methods
        /// <summary>
        /// Format a base and quote asset to a Biconomy recognized symbol (<c>BASE_QUOTE</c>).
        /// </summary>
        /// <param name="baseAsset">Base asset.</param>
        /// <param name="quoteAsset">Quote asset.</param>
        /// <param name="tradingMode">Trading mode.</param>
        /// <param name="deliverTime">Delivery time for delivery futures.</param>
        /// <returns>The exchange-formatted symbol.</returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            baseAsset = AssetAliases.CommonToExchangeName(baseAsset.ToUpperInvariant());
            quoteAsset = AssetAliases.CommonToExchangeName(quoteAsset.ToUpperInvariant());

            return baseAsset + "_" + quoteAsset;
        }
        #endregion
    }

    /// <summary>
    /// Rate limiter configuration for the Biconomy API.
    /// </summary>
    public class BiconomyRateLimiters
    {
        #region Properties
        /// <summary>
        /// Event for when a rate limit is triggered.
        /// </summary>
        public event Action<RateLimitEvent>? RateLimitTriggered;

        /// <summary>
        /// Event when the rate limit is updated.
        /// </summary>
        public event Action<RateLimitUpdateEvent>? RateLimitUpdated;

        /// <summary>
        /// REST rate limit gate.
        /// </summary>
        internal IRateLimitGate Rest { get; private set; } = null!;

        /// <summary>
        /// Socket rate limit gate.
        /// </summary>
        internal IRateLimitGate Socket { get; private set; } = null!;
        #endregion

        #region Constructors
        internal BiconomyRateLimiters()
        {
            Initialize();
        }
        #endregion

        #region Methods
        private void Initialize()
        {
            Rest = new RateLimitGate("Biconomy Rest");
            Socket = new RateLimitGate("Biconomy Socket")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new LimitItemTypeFilter(RateLimitItemType.Connection), 3, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));

            Rest.RateLimitTriggered += x => RateLimitTriggered?.Invoke(x);
            Rest.RateLimitUpdated += x => RateLimitUpdated?.Invoke(x);
            Socket.RateLimitTriggered += x => RateLimitTriggered?.Invoke(x);
            Socket.RateLimitUpdated += x => RateLimitUpdated?.Invoke(x);
        }
        #endregion
    }
}
