using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using System.Globalization;

namespace Biconomy.Net
{
    /// <summary>
    /// Authentication provider for the Biconomy REST API.
    /// </summary>
    internal class BiconomyAuthenticationProvider : AuthenticationProvider<BiconomyCredentials, BiconomyCredentials>
    {
        #region Statics
        private static readonly IStringMessageSerializer _messageSerializer = new SystemTextJsonMessageSerializer(BiconomyExchange._serializerContext);
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new Biconomy authentication provider.
        /// </summary>
        /// <param name="credentials">The Biconomy credentials.</param>
        public BiconomyAuthenticationProvider(BiconomyCredentials credentials)
            : base(credentials, credentials)
        { }
        #endregion

        #region Methods
        /// <inheritdoc />
        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration requestConfig)
        {
            if (!requestConfig.Authenticated)
                return;

            var timestamp = GetMillisecondTimestampLong(apiClient, false).ToString(CultureInfo.InvariantCulture);
            requestConfig.Headers ??= new Dictionary<string, string>();
            requestConfig.Headers["X-API-KEY"] = Key;
            requestConfig.Headers["X-API-TIMESTAMP"] = timestamp;

            var query = requestConfig.GetQueryString(false);
            var signPayload = string.IsNullOrEmpty(query)
                ? "timestamp=" + timestamp
                : query + "&timestamp=" + timestamp;

            if (requestConfig.BodyParameters != null)
            {
                var body = GetSerializedBody(_messageSerializer, requestConfig.BodyParameters);
                requestConfig.SetBodyContent(body);
                signPayload = body + "&timestamp=" + timestamp;
            }

            requestConfig.Headers["X-API-SIGN"] = SignHMACSHA256(signPayload, SignOutputType.Hex).ToLowerInvariant();
        }
        #endregion
    }
}
