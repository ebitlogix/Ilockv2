using ILock.Core.Data.Entities;

namespace ILock.Core.Services.Abstractions
{
    /// <summary>
    /// ProfileService Interface
    /// </summary>
    public interface IProfileService : IBaseEntityService<User>
    {
        /// <summary>
        /// Updates the.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Update(User user);
    }
}
