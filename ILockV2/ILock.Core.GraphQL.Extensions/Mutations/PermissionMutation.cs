using ILock.Core.Data;
using ILock.Core.Data.Entities;
using ILock.Core.GraphQL.Extensions.Types;
using Microsoft.EntityFrameworkCore;

namespace ILock.Core.GraphQL.Extensions.Mutations
{
    [ExtendObjectType(nameof(Mutation))]
    public class PermissionMutation : ILockMutationType
    {
        /// <summary>
        /// Assigns the permissions with role id.
        /// </summary>
        /// <param name="rolePermissionAssignmentPayload">The role permission assignment payload.</param>
        /// <param name="context">The context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public async Task<Data.Models.RolePermissionAssignmentPayload> AssignPermissionsWithRoleId(Data.Models.RolePermissionAssignmentPayload rolePermissionAssignmentPayload, [Service] AuthDBContext context, CancellationToken cancellationToken)
        {
            var isNewRole = false;
            var role = context.Roles.Include(r => r.Permissions).FirstOrDefault(p => p.ID == rolePermissionAssignmentPayload.RoleID);
            if (role == null)
            {
                isNewRole = true;
                role = context.Roles.Add(new Role()
                {
                    Name = rolePermissionAssignmentPayload.RoleName,
                    Permissions = new(),
                }).Entity;
            }

            var permissionsToDelete = role.Permissions.Where(p => !rolePermissionAssignmentPayload.FeatureIDs.ToList().Contains(p.FeatureID));


            for (int i = 0; i < rolePermissionAssignmentPayload.FeatureIDs.Count(); i++)
            {
                var availablePermission = context.Permissions.FirstOrDefault(p => p.FeatureID == rolePermissionAssignmentPayload.FeatureIDs[i] && p.Access == rolePermissionAssignmentPayload.AccessTypes[i]);
                if (availablePermission == null)
                {
                    availablePermission = context.Permissions.Add(new Permission
                    {

                        FeatureID = rolePermissionAssignmentPayload.FeatureIDs[i],
                        Access = rolePermissionAssignmentPayload.AccessTypes[i],
                    }
                    ).Entity;
                    context.Permissions.Add(availablePermission);
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

            //var permissions = context.Permissions.Where(p => rolePermissionAssignmentPayload.FeatureIDs.Select((value, index) => new { value, index }).Any(pair => pair.value == p.FeatureID && rolePermissionAssignmentPayload.AccessTypes[pair.index] == p.Access));

            //role.Permissions = permissions.Distinct().ToList();

            if (!isNewRole)
            {
                context.Roles.Update(role);
            }

            await context.SaveChangesAsync();
            rolePermissionAssignmentPayload.RoleID = role.ID;
            //await eventSender.SendAsync(nameof(EventSubscription.EventAdded), addEventPayload, cancellationToken);4
            return rolePermissionAssignmentPayload;
        }
    }
}
