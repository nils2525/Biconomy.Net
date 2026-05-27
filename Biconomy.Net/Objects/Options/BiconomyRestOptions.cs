using CryptoExchange.Net.Objects.Options;

namespace Biconomy.Net.Objects.Options
{
    /// <summary>
    /// Options for the Biconomy REST client.
    /// </summary>
    public class BiconomyRestOptions : RestExchangeOptions<BiconomyEnvironment, BiconomyCredentials>
    {
        #region Properties
        /// <summary>
        /// Default options for new clients.
        /// </summary>
        internal static BiconomyRestOptions Default { get; set; } = new()
        {
            Environment = BiconomyEnvironment.Live,
            AutoTimestamp = false
        };

        /// <summary>
        /// The receive window for authenticated requests.
        /// </summary>
        public int? ReceiveWindow { get; set; }

        /// <summary>
        /// Spot API options.
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new();
        #endregion

        #region Constructors
        /// <summary>
        /// ctor.
        /// </summary>
        public BiconomyRestOptions()
        {
            Default?.Set(this);
        }
        #endregion

        #region Methods
        internal BiconomyRestOptions Set(BiconomyRestOptions targetOptions)
        {
            targetOptions = base.Set<BiconomyRestOptions>(targetOptions);
            targetOptions.ReceiveWindow = ReceiveWindow;
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
        #endregion
    }
}
