using ILock.Core.Data;
using Microsoft.Extensions.Primitives;

namespace ILock.Core.Services.Abstractions
{
    /// <summary>
    /// Authentication Service Interface
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates the with password.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <param name="ipAddress">ipAddress.</param>
        /// <returns>An AuthenticationResult.</returns>
        AuthenticationResult AuthenticateWithPassword(IDictionary<string, StringValues> headers, string ipAddress);

        /// <summary>
        /// Authenticates the with s s o.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>An AuthenticationResult.</returns>
        AuthenticationResult AuthenticateWithSSO(int userId);
    }
}
