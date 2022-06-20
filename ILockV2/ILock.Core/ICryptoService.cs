namespace ILock.Core
{
    /// <summary>
    /// The crypto service.
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A string.</returns>
        string ComputeHash(string text);
        /// <summary>
        /// Computes the password.
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <param name="password">The password.</param>
        /// <returns>A string.</returns>
        string ComputePassword(string email, string password) => ComputeHash($"{email.ToUpper()}:{password}");
    }
}