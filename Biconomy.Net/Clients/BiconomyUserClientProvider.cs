using Biconomy.Net.Interfaces.Clients;
using Biconomy.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Biconomy.Net.Clients
{
    /// <inheritdoc />
    public class BiconomyUserClientProvider : IBiconomyUserClientProvider
    {
        #region Statics
        private static readonly ConcurrentDictionary<string, IBiconomyRestClient> _restClients = new();
        private static readonly ConcurrentDictionary<string, IBiconomySocketClient> _socketClients = new();
        #endregion

        #region Fields
        private readonly IOptions<BiconomyRestOptions> _restOptions;
        private readonly IOptions<BiconomySocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;
        #endregion

        #region Properties
        /// <inheritdoc />
        public string ExchangeName => BiconomyExchange.ExchangeName;
        #endregion

        #region Constructors
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients.</param>
        public BiconomyUserClientProvider(Action<BiconomyOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        { }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="httpClient">Optional shared HTTP client.</param>
        /// <param name="loggerFactory">Optional logger factory.</param>
        /// <param name="restOptions">REST client options.</param>
        /// <param name="socketOptions">Socket client options.</param>
        public BiconomyUserClientProvider(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<BiconomyRestOptions> restOptions, IOptions<BiconomySocketOptions> socketOptions)
        {
            _httpClient = httpClient ?? new HttpClient();
            _httpClient.Timeout = restOptions.Value.RequestTimeout;
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }
        #endregion

        #region Methods
        private IBiconomyRestClient CreateRestClient(string userIdentifier, BiconomyCredentials? credentials, BiconomyEnvironment? environment)
        {
            var client = new BiconomyRestClient(_httpClient, _loggerFactory, SetRestEnvironment(environment));
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }

            return client;
        }

        private IBiconomySocketClient CreateSocketClient(string userIdentifier, BiconomyCredentials? credentials, BiconomyEnvironment? environment)
        {
            var client = new BiconomySocketClient(SetSocketEnvironment(environment), _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }

            return client;
        }

        private IOptions<BiconomyRestOptions> SetRestEnvironment(BiconomyEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var options = new BiconomyRestOptions();
            _restOptions.Value.Set(options);
            options.Environment = environment;
            return Options.Create(options);
        }

        private IOptions<BiconomySocketOptions> SetSocketEnvironment(BiconomyEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var options = new BiconomySocketOptions();
            _socketOptions.Value.Set(options);
            options.Environment = environment;
            return Options.Create(options);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, BiconomyCredentials credentials, BiconomyEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public void ClearUserClients(string userIdentifier)
        {
            _restClients.TryRemove(userIdentifier, out _);
            _socketClients.TryRemove(userIdentifier, out _);
        }

        /// <inheritdoc />
        public IBiconomyRestClient GetRestClient(string userIdentifier, BiconomyCredentials? credentials = null, BiconomyEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public IBiconomySocketClient GetSocketClient(string userIdentifier, BiconomyCredentials? credentials = null, BiconomyEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }
        #endregion
    }
}
