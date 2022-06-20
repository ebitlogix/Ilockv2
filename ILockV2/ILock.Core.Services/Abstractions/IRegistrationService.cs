using ILock.Core.Data.Entities;

namespace ILock.Core.Services.Abstractions
{
    /// <summary>
    /// Registration Service Interface
    /// </summary>
    public interface IRegistrationService
    {
        /// <summary>
        /// Registers the.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>An int.</returns>
        public int Register(User user);
    }
}
