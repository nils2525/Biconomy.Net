using Biconomy.Net.Clients.MessageHandlers;
using Biconomy.Net.Interfaces.Clients.SpotApi;
using Biconomy.Net.Objects.Models.Socket;
using Biconomy.Net.Objects.Options;
using Biconomy.Net.Objects.Sockets;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;

namespace Biconomy.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IBiconomySocketClientSpotApi" />
    internal class BiconomySocketClientSpotApi : SocketApiClient<BiconomyEnvironment, BiconomyAuthenticationProvider, BiconomyCredentials>, IBiconomySocketClientSpotApi
    {
        #region Properties
        /// <summary>
        /// Strongly-typed client options.
        /// </summary>
        public new BiconomySocketOptions ClientOptions => (BiconomySocketOptions)base.ClientOptions;
        #endregion

        #region Constructors
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="options">Options.</param>
        internal BiconomySocketClientSpotApi(ILogger logger, BiconomySocketOptions options)
            : base(logger, options.Environment.SocketClientAddress, options, options.SpotOptions)
        {
            RateLimiter = BiconomyExchange.RateLimiter.Socket;
            RegisterPeriodicQuery(
                "Ping",
                TimeSpan.FromSeconds(10),
                _ => new BiconomyPingQuery(),
                (connection, result) =>
                {
                    if (result.Error?.ErrorType == ErrorType.Timeout)
                    {
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
                });
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer()
            => new SystemTextJsonMessageSerializer(BiconomyExchange._serializerContext);

        /// <inheritdoc />
        protected override BiconomyAuthenticationProvider CreateAuthenticationProvider(BiconomyCredentials credentials)
            => new(credentials);

        /// <inheritdoc />
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType)
            => new BiconomySocketMessageHandler();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => BiconomyExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string[] symbols, Action<DataEvent<BiconomyStateUpdateEnvelope>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, BiconomyStateUpdateEnvelope>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<BiconomyStateUpdateEnvelope>(BiconomyExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId("state.update")
                        .WithSymbol(data.Symbol));
            });

            var subscription = new BiconomyTickerSubscription(_logger, symbols, internalHandler);
            return SubscribeAsync(BaseAddress, subscription, ct);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string[] symbols, Action<DataEvent<BiconomyDealsUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, BiconomyDealsUpdate>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<BiconomyDealsUpdate>(BiconomyExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId("deals.update")
                        .WithSymbol(data.Symbol));
            });

            var subscription = new BiconomyTradeSubscription(_logger, symbols, internalHandler);
            return SubscribeAsync(BaseAddress, subscription, ct);
        }
        #endregion
    }
}
