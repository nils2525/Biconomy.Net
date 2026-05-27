using Biconomy.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;

namespace Biconomy.Net.Objects.Sockets
{
    /// <summary>
    /// Subscription for the Biconomy <c>deals</c> websocket channel.
    /// </summary>
    internal class BiconomyTradeSubscription : Subscription
    {
        #region Fields
        private readonly Action<DateTime, string?, BiconomyDealsUpdate> _handler;
        private readonly string[] _symbols;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new trade subscription.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="symbols">Symbols to subscribe to.</param>
        /// <param name="handler">Update handler.</param>
        public BiconomyTradeSubscription(ILogger logger, string[] symbols, Action<DateTime, string?, BiconomyDealsUpdate> handler)
            : base(logger, false)
        {
            _symbols = symbols;
            _handler = handler;
            IndividualSubscriptionCount = symbols.Length;

            var routes = new List<MessageRoute>();
            foreach (var symbol in symbols)
                routes.Add(MessageRoute<BiconomyDealsUpdate>.CreateWithTopicFilter("deals.update", symbol, DoHandleMessage));

            MessageRouter = MessageRouter.Create(routes.ToArray());
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
            => new BiconomyQuery(new BiconomySocketRequest
            {
                Method = "deals.subscribe",
                Params = _symbols.Cast<object>().ToArray()
            }, Authenticated);

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
            => new BiconomyQuery(new BiconomySocketRequest
            {
                Method = "deals.unsubscribe",
                Params = []
            }, Authenticated);

        /// <summary>
        /// Handle update messages.
        /// </summary>
        /// <param name="connection">Socket connection.</param>
        /// <param name="receiveTime">Receive time.</param>
        /// <param name="originalData">Original JSON.</param>
        /// <param name="message">Parsed message.</param>
        /// <returns>Success result.</returns>
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BiconomyDealsUpdate message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
        #endregion
    }
}
