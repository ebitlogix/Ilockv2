using ILock.Core.Data;
using ILock.Core.Data.Entities;
using ILock.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace ILock.Core.Services.Implmentations
{
    /// <summary>
    /// The user service.
    /// </summary>
    public class UserService : BaseEntityService<User>, IUserService
    {
        private readonly AuthDBContext authDBContext;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="authDBContext">The auth d b context.</param>
        public UserService(AuthDBContext authDBContext, ILoggerFactory loggerFactory) 
            : base(authDBContext)
        {
            this.authDBContext = authDBContext;
            this.logger = loggerFactory.CreateLogger<UserService>();
        }

        /// <summary>
        /// Gets the user id by email address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>An int.</returns>
        public int? GetUserIdByEmailAddress(string email)
        {
            var user = FirstOrDefault(u => u.Email.Equals(email));
            return user == null ? null : user.ID;
        }


        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="includeChildren">If true, include children.</param>
        /// <returns>An User.</returns>
        public User GetById(int id, bool includeChildren)
        {
            if (includeChildren)
            {
                return this.authDBContext.Users.Include(u => u.Roles).Include(u => u.Policies).FirstOrDefault(u => u.ID == id);
            }
            return GetById(id);
        }

        /// <summary>
        /// Gets the user id by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>An int.</returns>
        public int GetUserIdByUsername(string username)
        {
            return FirstOrDefault(u => u.Username.Equals(username)).ID;
        }

        /// <summary>
        /// Gets the users roles.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A list of Data.Entities.Role.</returns>
        public IEnumerable<Data.Entities.Role> GetUsersRoles(int userId)
        {
            return authDBContext.Users.Include(u => u.Roles).FirstOrDefault(u => u.ID == userId).Roles.ToList();
        }

        /// <summary>
        /// Updates the User
        /// </summary>
        /// <param name="entity">The user.</param>
        /// <returns>An EntityEntry.</returns>
        public new User Update(User entity)
        {
            var user = authDBContext.Users.AsNoTracking().FirstOrDefault(u => u.ID == entity.ID);
            if (user == null)
            {
                return null;
            }

            entity.Password = user.Password;
            authDBContext.Users.Update(entity);
            return entity;
        }

        /// <summary>
        /// Updates the User Asynchronously
        /// </summary>
        /// <param name="entity">The user.</param>
        /// <returns>An EntityEntry.</returns>
        public new Task<User> UpdateAsync(User entity) => Task.FromResult(Update(entity));
    }
}
