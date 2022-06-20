using ILock.Core.Data.Entities;
using ILock.Core.GraphQL.Demo.Data;
using ILock.Core.GraphQL.Extensions.Queries;
using ILock.Core.Services.Abstractions;
using System.Security.Claims;

namespace ILock.Core.GraphQL.Demo.Queries
{
    /// <summary>
    /// The queries related to User module.
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class UserAssociationQueryResolver
    {
        /// <summary>
        /// Gets the user model selection.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="roleService">The role service.</param>
        /// <param name="userService">The user service.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName(nameof(GetUserModelSelection))]
        public IEnumerable<Data.Models.ModelSelection> GetUserModelSelection(
            [Service] DemoDbContext appDbContext,
            ClaimsPrincipal claimsPrincipal,
            [Service] IRoleService roleService,
            [Service] IUserService userService)
        {
            var username = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRoles = claimsPrincipal.FindAll(ClaimTypes.Role);

            var userID = userService.GetUserIdByUsername(username);

            var roleIDs = roleService.GetRoleIdsByNames(userRoles.Select(r => r.Value).ToList());
            return appDbContext.UserRoleScopeAssociations.Where(u => u.UserID == userID && roleIDs.Contains(u.RoleID)).Select(u =>
            new Data.Models.ModelSelection
            {
                Country = u.Country,
                Retailer = u.Retailer,
                Scenario = u.Scenario
            }).Distinct().ToList();
        }

        /// <summary>
        /// Gets the user model based permissions.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="permissionService">The permission service.</param>
        /// <param name="countryID">The country i d.</param>
        /// <param name="retailerID">The retailer i d.</param>
        /// <param name="scenarioID">The scenario i d.</param>
        /// <returns>A list of Permissions.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("GetUserModelBasedPermissions")]
        public IEnumerable<Permission> GetUserModelBasedPermissions(
            [Service] DemoDbContext appDbContext,
            ClaimsPrincipal claimsPrincipal,
            [Service] IUserService userService,
            [Service] IPermissionService permissionService,
            int countryID,
            int retailerID,
            int scenarioID)
        {
            var username = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            var userID = userService.GetUserIdByUsername(username);

            var modelAssociatedRoleIDs = appDbContext.UserRoleScopeAssociations.Where(u => u.UserID == userID && u.CountryID == countryID && u.RetailerID == retailerID && u.ScenarioID == scenarioID).Select(u => u.RoleID).Distinct().ToArray();

            return permissionService.GetDistinctPermissionsWithRoles(modelAssociatedRoleIDs);
        }

        /// <summary>
        /// Gets the user model based roles.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="roleService">The role service.</param>
        /// <param name="userID">userID</param>
        /// <param name="countryID">The country i d.</param>
        /// <param name="retailerID">The retailer i d.</param>
        /// <param name="scenarioID">The scenario i d.</param>
        /// <returns>A list of Roles.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("GetUserModelBasedRoles")]
        public IEnumerable<Role> GetUserModelBasedRoles(
            [Service] DemoDbContext appDbContext,
            [Service] IUserService userService,
            [Service] IRoleService roleService,
            int userID,
            int countryID,
            int retailerID,
            int scenarioID)
        {
            var modelAssociatedRoleIDs = appDbContext.UserRoleScopeAssociations
                .Where(u => u.UserID == userID && u.CountryID == countryID && u.RetailerID == retailerID && u.ScenarioID == scenarioID)
                .Select(u => u.RoleID).ToArray();

            return roleService.GetRoleNamesByIds(modelAssociatedRoleIDs);
        }
    }
}
