using Biconomy.Net.Objects;
using CryptoExchange.Net.Objects;

namespace Biconomy.Net
{
    /// <summary>
    /// Biconomy environments.
    /// </summary>
    public class BiconomyEnvironment : TradeEnvironment
    {
        #region Properties
        /// <summary>
        /// REST API base address.
        /// </summary>
        public string RestClientAddress { get; }

        /// <summary>
        /// Socket API base address.
        /// </summary>
        public string SocketClientAddress { get; }

        /// <summary>
        /// Available environment names.
        /// </summary>
        public static string[] All => [Live.Name];

        /// <summary>
        /// Live environment.
        /// </summary>
        public static BiconomyEnvironment Live { get; } = new(
            TradeEnvironmentNames.Live,
            BiconomyApiAddresses.Default.RestClientAddress,
            BiconomyApiAddresses.Default.SocketClientAddress);
        #endregion

        #region Constructors
        /// <summary>
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment.
        /// </summary>
#pragma warning disable CS8618
        public BiconomyEnvironment()
            : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618
        { }

        internal BiconomyEnvironment(string name, string restAddress, string socketAddress)
            : base(name)
        {
            RestClientAddress = restAddress;
            SocketClientAddress = socketAddress;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the Biconomy environment by name.
        /// </summary>
        /// <param name="name">Environment name.</param>
        /// <returns>The environment, or <c>null</c> when unknown.</returns>
        public static BiconomyEnvironment? GetEnvironmentByName(string? name)
            => name switch
            {
                TradeEnvironmentNames.Live => Live,
                "" => Live,
                null => Live,
                _ => null
            };

        /// <summary>
        /// Create a custom environment.
        /// </summary>
        /// <param name="name">Environment name.</param>
        /// <param name="restAddress">REST base address.</param>
        /// <param name="socketAddress">WebSocket address.</param>
        /// <returns>The custom environment.</returns>
        public static BiconomyEnvironment CreateCustom(string name, string restAddress, string socketAddress)
            => new(name, restAddress, socketAddress);
        #endregion
    }
}
