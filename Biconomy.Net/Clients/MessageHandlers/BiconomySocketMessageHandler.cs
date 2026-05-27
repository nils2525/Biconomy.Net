using Biconomy.Net.Objects.Models.Socket;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using System.Text.Json;

namespace Biconomy.Net.Clients.MessageHandlers
{
    /// <summary>
    /// Socket message handler for Biconomy spot streams.
    /// </summary>
    internal class BiconomySocketMessageHandler : JsonSocketMessageHandler
    {
        #region Properties
        /// <inheritdoc />
        public override JsonSerializerOptions Options { get; } = BiconomyExchange._serializerContext;

        /// <inheritdoc />
        protected override MessageTypeDefinition[] TypeEvaluators { get; } =
        [
            new MessageTypeDefinition
            {
                Fields =
                [
                    new PropertyFieldReference("method")
                ],
                TypeIdentifierCallback = x => x.FieldValue("method")!
            },
            new MessageTypeDefinition
            {
                Fields =
                [
                    new PropertyFieldReference("id").WithNotNullConstraint()
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!
            }
        ];
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new socket message handler.
        /// </summary>
        public BiconomySocketMessageHandler()
        {
            AddTopicMapping<BiconomyStateUpdateEnvelope>(x => x.Symbol);
            AddTopicMapping<BiconomyDealsUpdate>(x => x.Symbol);
        }
        #endregion
    }
}
