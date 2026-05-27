using CryptoExchange.Net.Objects.Errors;

namespace Biconomy.Net
{
    /// <summary>
    /// Biconomy error catalogs. Intentionally empty; wrapper-side error mapping is test-driven.
    /// </summary>
    public static class BiconomyErrors
    {
        #region Properties
        /// <summary>
        /// Empty REST error mapping.
        /// </summary>
        public static ErrorMapping SpotRestErrors { get; } = new([]);

        /// <summary>
        /// Empty socket error mapping.
        /// </summary>
        public static ErrorMapping SpotSocketErrors { get; } = new([]);
        #endregion
    }
}
