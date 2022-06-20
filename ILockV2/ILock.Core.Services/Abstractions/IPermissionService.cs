using ILock.Core.Data.Entities;
using ILock.Core.Data.Models;

namespace ILock.Core.Services.Abstractions
{
    /// <summary>
    /// The permission service.
    /// </summary>
    public interface IPermissionService : IBaseEntityService<Permission>
    {
        /// <summary>
        /// Assigns the permissions with role id.
        /// </summary>
        /// <param name="rolePermissionAssignmentPayload">The role permission assignment payload.</param>
        /// <returns>A Task.</returns>
        Task<RolePermissionAssignmentPayload> AssignPermissionsWithRoleId(RolePermissionAssignmentPayload rolePermissionAssignmentPayload);

        /// <summary>
        /// Gets the distinct permissions with roles.
        /// </summary>
        /// <param name="roleIDs">The role i ds.</param>
        /// <returns>A list of Permissions.</returns>
        IEnumerable<Permission> GetDistinctPermissionsWithRoles(IEnumerable<int> roleIDs);
        /// <summary>
        /// Gets the permissions by role id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A list of Permissions.</returns>
        IEnumerable<Permission> GetPermissionsByRoleId(int id);

        /// <summary>
        /// Gets the roles associated with permission.
        /// </summary>
        /// <param name="permissionName">The permission name.</param>
        /// <returns>A list of Roles.</returns>
        IEnumerable<Role> GetRolesAssociatedWithPermission(string permissionName);

        /// <summary>
        /// Gets the roles associated with permission and accesslevels
        /// </summary>
        /// <param name="permissionName">Permission Name</param>
        /// <param name="accessLevels">AccessLevels</param>
        /// <returns>A list of Roles.</returns>
        IEnumerable<Role> GetRolesAssociatedWithPermission(string permissionName, IEnumerable<string> accessLevels);
    }
}