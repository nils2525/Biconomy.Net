using CryptoExchange.Net.Objects.Options;

namespace Biconomy.Net.Objects.Options
{
    /// <summary>
    /// Options for the Biconomy socket client.
    /// </summary>
    public class BiconomySocketOptions : SocketExchangeOptions<BiconomyEnvironment, BiconomyCredentials>
    {
        #region Properties
        /// <summary>
        /// Default options for new clients.
        /// </summary>
        internal static BiconomySocketOptions Default { get; set; } = new()
        {
            Environment = BiconomyEnvironment.Live,
            SocketSubscriptionsCombineTarget = 1,
        };

        /// <summary>
        /// Spot API options.
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new();
        #endregion

        #region Constructors
        /// <summary>
        /// ctor.
        /// </summary>
        public BiconomySocketOptions()
        {
            Default?.Set(this);
        }
        #endregion

        #region Methods
        internal BiconomySocketOptions Set(BiconomySocketOptions targetOptions)
        {
            targetOptions = base.Set<BiconomySocketOptions>(targetOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
        #endregion
    }
}
