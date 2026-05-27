using Biconomy.Net;
using Biconomy.Net.Clients;
using Biconomy.Net.Interfaces.Clients;
using Biconomy.Net.Objects.Options;
using CryptoExchange.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region Methods
        /// <summary>
        /// Add Biconomy services using configuration.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">Configuration section.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddBiconomy(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new BiconomyOptions();
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            try
            {
                configuration.Bind(options);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Invalid configuration provided", ex);
            }

            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            var restEnvName = options.Rest.Environment?.Name ?? options.Environment?.Name ?? BiconomyEnvironment.Live.Name;
            var socketEnvName = options.Socket.Environment?.Name ?? options.Environment?.Name ?? BiconomyEnvironment.Live.Name;
            options.Rest.Environment = BiconomyEnvironment.GetEnvironmentByName(restEnvName) ?? options.Rest.Environment!;
            options.Rest.ApiCredentials ??= options.ApiCredentials;
            options.Socket.Environment = BiconomyEnvironment.GetEnvironmentByName(socketEnvName) ?? options.Socket.Environment!;
            options.Socket.ApiCredentials ??= options.ApiCredentials;

            services.AddSingleton(_ => Options.Options.Create(options.Rest));
            services.AddSingleton(_ => Options.Options.Create(options.Socket));

            return AddBiconomyCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add Biconomy services using options.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="optionsDelegate">Options delegate.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddBiconomy(this IServiceCollection services, Action<BiconomyOptions>? optionsDelegate = null)
        {
            var options = new BiconomyOptions();
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            optionsDelegate?.Invoke(options);

            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            options.Rest.Environment ??= options.Environment ?? BiconomyEnvironment.Live;
            options.Rest.ApiCredentials ??= options.ApiCredentials;
            options.Socket.Environment ??= options.Environment ?? BiconomyEnvironment.Live;
            options.Socket.ApiCredentials ??= options.ApiCredentials;

            services.AddSingleton(_ => Options.Options.Create(options.Rest));
            services.AddSingleton(_ => Options.Options.Create(options.Socket));

            return AddBiconomyCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddBiconomyCore(this IServiceCollection services, ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IBiconomyRestClient, BiconomyRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<BiconomyRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new BiconomyRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<BiconomyRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<BiconomyRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);

            services.Add(new ServiceDescriptor(typeof(IBiconomySocketClient), x => new BiconomySocketClient(x.GetRequiredService<IOptions<BiconomySocketOptions>>(), x.GetRequiredService<ILoggerFactory>()), socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddSingleton<IBiconomyUserClientProvider, BiconomyUserClientProvider>(x =>
                new BiconomyUserClientProvider(
                    x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(IBiconomyRestClient).Name),
                    x.GetRequiredService<ILoggerFactory>(),
                    x.GetRequiredService<IOptions<BiconomyRestOptions>>(),
                    x.GetRequiredService<IOptions<BiconomySocketOptions>>()));

            return services;
        }
        #endregion
    }
}
