using ILock.Core.Data;
using ILock.Core.Data.Entities;
using ILock.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ILock.Core.Services.Implmentations
{
    /// <summary>
    /// Permission Service
    /// </summary>
    public class PermissionService : BaseEntityService<Permission>, IPermissionService
    {
        private readonly AuthDBContext dbContext;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public PermissionService(AuthDBContext dbContext, ILoggerFactory loggerFactory)
            : base(dbContext)
        {
            this.dbContext = dbContext;
            this.logger = loggerFactory.CreateLogger<PermissionService>();
        }

        /// <summary>
        /// Gets the roles associated with permission.
        /// </summary>
        /// <param name="permissionName">The permission name.</param>
        /// <param name="accessLevels">The access levels.</param>
        /// <returns>A list of Roles.</returns>
        public IEnumerable<Role> GetRolesAssociatedWithPermission(string permissionName, IEnumerable<string> accessLevels)
        {
            var permission = this.dbContext.Permissions.Include(p => p.Roles).FirstOrDefault(p => p.Feature.Name == permissionName && accessLevels.Contains(p.Access));

            if (permission != null)
            {
                return permission.Roles;
            }

            return new List<Role>();
        }

        /// <summary>
        /// Gets the roles associated with permission.
        /// </summary>
        /// <param name="permissionName">The permission name.</param>
        /// <returns>A list of Roles.</returns>
        public IEnumerable<Role> GetRolesAssociatedWithPermission(string permissionName)
        {
            var roles = this.dbContext.Permissions.Include(p => p.Roles).FirstOrDefault(p => p.Feature.Name == permissionName).Roles;
            return roles;
        }

        /// <summary>
        /// Gets the distinct permissions with roles.
        /// </summary>
        /// <param name="roleIDs">The role i ds.</param>
        /// <returns>A list of Roles.</returns>
        public IEnumerable<Permission> GetDistinctPermissionsWithRoles(IEnumerable<int> roleIDs)
        {
            var permissions = this.dbContext.Permissions.Where(p => p.Roles.Any(r => roleIDs.Contains(r.ID))).Include(p => p.Feature).Distinct();
            return permissions;
        }

        /// <summary>
        /// Gets the permissions by role id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A list of Permissions.</returns>
        public IEnumerable<Permission> GetPermissionsByRoleId(int id)
        {
            return dbContext.Permissions.Include(p => p.Feature).Where(p => p.Roles.Any(r => r.ID == id)).ToList();
        }

        /// <summary>
        /// Assigns the permissions with role id.
        /// </summary>
        /// <param name="rolePermissionAssignmentPayload">The role permission assignment payload.</param>
        /// <returns>A Task.</returns>
        public async Task<Data.Models.RolePermissionAssignmentPayload> AssignPermissionsWithRoleId(Data.Models.RolePermissionAssignmentPayload rolePermissionAssignmentPayload)
        {
            var isNewRole = false;
            var role = dbContext.Roles.Include(r => r.Permissions).FirstOrDefault(p => p.ID == rolePermissionAssignmentPayload.RoleID);
            if (role == null)
            {
                isNewRole = true;
                role = dbContext.Roles.Add(new Role()
                {
                    Name = rolePermissionAssignmentPayload.RoleName,
                    Permissions = new(),
                }).Entity;
            }

            var permissionsToDelete = role.Permissions.Where(p => !rolePermissionAssignmentPayload.FeatureIDs.ToList().Contains(p.FeatureID));


            for (int i = 0; i < rolePermissionAssignmentPayload.FeatureIDs.Count(); i++)
            {
                var availablePermission = dbContext.Permissions.FirstOrDefault(p => p.FeatureID == rolePermissionAssignmentPayload.FeatureIDs[i] && p.Access == rolePermissionAssignmentPayload.AccessTypes[i]);
                if (availablePermission == null)
                {
                    availablePermission = dbContext.Permissions.Add(new Permission
                    {

                        FeatureID = rolePermissionAssignmentPayload.FeatureIDs[i],
                        Access = rolePermissionAssignmentPayload.AccessTypes[i],
                    }
                    ).Entity;
                    dbContext.Permissions.Add(availablePermission);
                }

                if (isNewRole || !role.Permissions.Contains(availablePermission))
                {
                    role.Permissions.Add(availablePermission);
                }
            }

            if (permissionsToDelete != null && permissionsToDelete.ToList().Count > 0)
            {
                role.Permissions.RemoveAll(p => permissionsToDelete.ToList().Contains(p));
            }


            if (!isNewRole)
            {
                dbContext.Roles.Update(role);
            }

            await dbContext.SaveChangesAsync();
            rolePermissionAssignmentPayload.RoleID = role.ID;
            return rolePermissionAssignmentPayload;
        }
    }
}
