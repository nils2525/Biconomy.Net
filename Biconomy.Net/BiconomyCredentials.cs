using CryptoExchange.Net.Authentication;

namespace Biconomy.Net
{
    /// <summary>
    /// Biconomy API credentials.
    /// </summary>
    public class BiconomyCredentials : HMACCredential
    {
        #region Constructors
        /// <summary>
        /// Create new credentials.
        /// </summary>
        public BiconomyCredentials()
        { }

        /// <summary>
        /// Create new credentials providing credentials in HMAC format.
        /// </summary>
        /// <param name="key">API key.</param>
        /// <param name="secret">API secret.</param>
        public BiconomyCredentials(string key, string secret)
            : base(key, secret)
        { }

        /// <summary>
        /// Create new credentials from HMAC credentials.
        /// </summary>
        /// <param name="credential">HMAC credentials.</param>
        public BiconomyCredentials(HMACCredential credential)
            : base(credential.Key, credential.Secret)
        { }
        #endregion

        #region Methods
        /// <summary>
        /// Specify the HMAC credentials.
        /// </summary>
        /// <param name="key">API key.</param>
        /// <param name="secret">API secret.</param>
        /// <returns>This credentials instance for chaining.</returns>
        public BiconomyCredentials WithHMAC(string key, string secret)
        {
            if (!string.IsNullOrEmpty(Key))
                throw new InvalidOperationException("Credentials already set");

            Key = key;
            Secret = secret;
            return this;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy()
            => new BiconomyCredentials(this);
        #endregion
    }
}
