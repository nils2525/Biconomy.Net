using Biconomy.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Biconomy.Net.Clients.MessageHandlers
{
    /// <summary>
    /// REST message handler for Biconomy unified response envelopes.
    /// </summary>
    internal class BiconomyRestMessageHandler : JsonRestMessageHandler
    {
        #region Fields
        private readonly ErrorMapping _errorMapping;
        #endregion

        #region Properties
        /// <inheritdoc />
        public override JsonSerializerOptions Options { get; } = BiconomyExchange._serializerContext;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new Biconomy REST message handler.
        /// </summary>
        /// <param name="errorMapping">Error mapping.</param>
        public BiconomyRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        public override Error? CheckDeserializedResponse<T>(HttpResponseHeaders responseHeaders, T result)
        {
            if (result is not BiconomyResponse response)
                return null;

            if (response.Code == 0)
                return null;

            var code = response.Code.ToString();
            return new ServerError(code, _errorMapping.GetErrorInfo(code, response.Message));
        }

        /// <inheritdoc />
        public override async ValueTask<Error> ParseErrorResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (error, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (error != null)
                return error;

            var root = document!.RootElement;
            var code = root.TryGetProperty("code", out var codeProperty) ? codeProperty.ToString() : httpStatusCode.ToString();
            var message = root.TryGetProperty("message", out var messageProperty) ? messageProperty.GetString() : null;
            return new ServerError(code, _errorMapping.GetErrorInfo(code, message));
        }
        #endregion
    }
}
