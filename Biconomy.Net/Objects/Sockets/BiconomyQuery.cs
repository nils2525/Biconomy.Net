using Biconomy.Net.Objects.Models.Socket;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;

namespace Biconomy.Net.Objects.Sockets
{
    /// <summary>
    /// Biconomy JSON-RPC subscribe/unsubscribe query.
    /// </summary>
    internal class BiconomyQuery : Query<BiconomySocketResponse<BiconomySocketStatus>>
    {
        #region Constructors
        /// <summary>
        /// Create a new Biconomy query.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="authenticated">Whether the query is authenticated.</param>
        /// <param name="weight">Request weight.</param>
        public BiconomyQuery(BiconomySocketRequest request, bool authenticated, int weight = 1)
            : base(AssignRequestId(request), authenticated, weight)
        {
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<BiconomySocketResponse<BiconomySocketStatus>>(request.Id.ToString(), HandleMessage);
        }
        #endregion

        #region Methods
        private static BiconomySocketRequest AssignRequestId(BiconomySocketRequest request)
        {
            request.Id = ExchangeHelpers.NextId();
            return request;
        }

        /// <summary>
        /// Handle the query response.
        /// </summary>
        /// <param name="connection">Socket connection.</param>
        /// <param name="receiveTime">Receive time.</param>
        /// <param name="originalData">Original JSON.</param>
        /// <param name="message">Parsed message.</param>
        /// <returns>Query result.</returns>
        public CallResult<BiconomySocketResponse<BiconomySocketStatus>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BiconomySocketResponse<BiconomySocketStatus> message)
        {
            if (message.ErrorMessage != null)
                return new CallResult<BiconomySocketResponse<BiconomySocketStatus>>(new ServerError(ErrorInfo.Unknown with { Message = message.ErrorMessage }));

            return new CallResult<BiconomySocketResponse<BiconomySocketStatus>>(message, originalData, null);
        }
        #endregion
    }
}
