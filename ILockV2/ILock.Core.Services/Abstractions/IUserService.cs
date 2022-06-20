// <copyright file="IUserService.cs" company="Retail Insight Ltd">
// Copyright (c) Retail Insight Ltd. All rights reserved.
// </copyright>

using ILock.Core.Data.Entities;

namespace ILock.Core.Services.Abstractions
{
    /// <summary>
    /// The user service interface.
    /// </summary>
    public interface IUserService : IBaseEntityService<User>
    {
        /// <summary>
        /// Gets the user id by email address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>An int.</returns>
        int? GetUserIdByEmailAddress(string email);

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="includeChildren">If true, include children.</param>
        /// <returns>An User.</returns>
        User GetById(int id, bool includeChildren);

        /// <summary>
        /// Gets the user id by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>An int.</returns>
        int GetUserIdByUsername(string username);

        /// <summary>
        /// Gets the users roles.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A list of Roles.</returns>
        IEnumerable<Role> GetUsersRoles(int userId);

        /// <summary>
        /// Updates the User, new method ignoring base
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>An User.</returns>
        new User Update(User entity);

        /// <summary>
        /// Updates the async.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task.</returns>
        new Task<User> UpdateAsync(User entity);
    }
}