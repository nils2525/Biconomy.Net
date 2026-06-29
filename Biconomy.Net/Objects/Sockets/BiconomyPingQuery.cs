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
    /// Biconomy websocket heartbeat query.
    /// </summary>
    internal class BiconomyPingQuery : Query<BiconomySocketResponse<string>>
    {
        #region Constructors
        /// <summary>
        /// Create a new ping query.
        /// </summary>
        public BiconomyPingQuery()
            : base(BuildRequest(), false, 0)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageRouter = MessageRouter.CreateForQuery<BiconomySocketResponse<string>>(((BiconomySocketRequest)Request).Id.ToString(), HandleMessage);
        }
        #endregion

        #region Methods
        private static BiconomySocketRequest BuildRequest()
            => new()
            {
                Id = ExchangeHelpers.NextId(),
                Method = "server.ping",
                Params = []
            };

        /// <summary>
        /// Handle the ping response.
        /// </summary>
        /// <param name="connection">Socket connection.</param>
        /// <param name="receiveTime">Receive time.</param>
        /// <param name="originalData">Original JSON.</param>
        /// <param name="message">Parsed message.</param>
        /// <returns>Query result.</returns>
        public CallResult<BiconomySocketResponse<string>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BiconomySocketResponse<string> message)
        {
            if (message.ErrorMessage != null)
                return CallResult.Fail<BiconomySocketResponse<string>>(new ServerError(ErrorInfo.Unknown with { Message = message.ErrorMessage }));

            return CallResult.Ok(message, originalData);
        }
        #endregion
    }
}
