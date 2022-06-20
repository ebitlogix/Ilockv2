// <copyright file="PermissionQueryResolver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using ILock.Core.Data;
using ILock.Core.GraphQL.Extensions.Types;
using Microsoft.EntityFrameworkCore;

namespace ILock.Core.GraphQL.Extensions.Queries
{
    /// <summary>
    /// The queries related to Permission module.
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class PermissionQueryResolver : ILockQueryType
    {
        /// <summary>
        /// Gets the Permissions.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName(nameof(GetPermissions))]
        public IQueryable<Data.Entities.Permission> GetPermissions([Service] AuthDBContext appDbContext)
        {
            return appDbContext.Permissions;
        }

        /// <summary>
        /// Gets the event by id.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <param name="id">The id.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName(nameof(GetPermissionsByRoleId))]
        public IQueryable<Data.Entities.Permission> GetPermissionsByRoleId([Service] AuthDBContext appDbContext, int id)
        {
            return appDbContext.Permissions.Include(p => p.Feature).Where(p => p.Roles.Any(r => r.ID == id)).AsQueryable();
        }

        ///// <summary>
        ///// Gets the event by id.
        ///// </summary>
        ///// <param name="appDbContext">The app db context.</param>
        ///// <param name="id">The id.</param>
        ///// <returns>An IQueryable.</returns>
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLName("GetUserPermissions")]
        //public IQueryable<Data.Entities.Permission> GetUserPermissions([Service] AuthDBContext appDbContext, ClaimsPrincipal claimsPrincipal)
        //{
        //    var userRoles = claimsPrincipal.FindAll(ClaimTypes.Role);

        //    return appDbContext.Permissions.Include(p => p.Feature).Where(p => p.Roles.).AsQueryable();

        //    return appDbContext.Roles.Include(r => r.Permissions).FirstOrDefault(p => p.ID == id).Permissions.AsQueryable().Include(p => p.Feature);
        //}
    }
}
