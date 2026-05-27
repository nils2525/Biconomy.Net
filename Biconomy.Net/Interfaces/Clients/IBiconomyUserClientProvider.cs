namespace Biconomy.Net.Interfaces.Clients
{
    /// <summary>
    /// Biconomy user client provider.
    /// </summary>
    public interface IBiconomyUserClientProvider
    {
        /// <summary>
        /// Initialize a client for the specified user identifier.
        /// </summary>
        /// <param name="userIdentifier">The identifier for the user.</param>
        /// <param name="credentials">The credentials for the user.</param>
        /// <param name="environment">The environment to use.</param>
        void InitializeUserClient(string userIdentifier, BiconomyCredentials credentials, BiconomyEnvironment? environment = null);

        /// <summary>
        /// Reset the cached clients for a user.
        /// </summary>
        /// <param name="userIdentifier">The identifier for the user.</param>
        void ClearUserClients(string userIdentifier);

        /// <summary>
        /// Get the REST client for a specific user.
        /// </summary>
        /// <param name="userIdentifier">The identifier for the user.</param>
        /// <param name="credentials">The credentials for the user.</param>
        /// <param name="environment">The environment to use.</param>
        /// <returns>The REST client.</returns>
        IBiconomyRestClient GetRestClient(string userIdentifier, BiconomyCredentials? credentials = null, BiconomyEnvironment? environment = null);

        /// <summary>
        /// Get the socket client for a specific user.
        /// </summary>
        /// <param name="userIdentifier">The identifier for the user.</param>
        /// <param name="credentials">The credentials for the user.</param>
        /// <param name="environment">The environment to use.</param>
        /// <returns>The socket client.</returns>
        IBiconomySocketClient GetSocketClient(string userIdentifier, BiconomyCredentials? credentials = null, BiconomyEnvironment? environment = null);
    }
}
