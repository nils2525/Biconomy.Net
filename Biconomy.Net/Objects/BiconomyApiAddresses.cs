namespace Biconomy.Net.Objects
{
    /// <summary>
    /// Biconomy API addresses.
    /// </summary>
    public class BiconomyApiAddresses
    {
        #region Properties
        /// <summary>
        /// REST API base address.
        /// </summary>
        public string RestClientAddress { get; set; } = string.Empty;

        /// <summary>
        /// WebSocket API base address.
        /// </summary>
        public string SocketClientAddress { get; set; } = string.Empty;

        /// <summary>
        /// Default Biconomy API addresses.
        /// </summary>
        public static BiconomyApiAddresses Default { get; } = new()
        {
            RestClientAddress = "https://api.biconomy.com",
            SocketClientAddress = "wss://bei.biconomy.com/ws"
        };
        #endregion
    }
}
