using ILock.Core.Data.Entities;
using ILock.Core.GraphQL.Demo.Data;
using ILock.Core.GraphQL.Extensions.Mutations;
using ILock.Core.Services.Abstractions;

namespace ILock.Core.GraphQL.Demo.Mutations
{
    /// <summary>
    /// The UserRole module mutations.
    /// </summary>
    [ExtendObjectType(nameof(Mutation))]
    public class UserRoleMutation
    {
        /// <summary>
        /// Assigns the user role.
        /// </summary>
        /// <param name="userRoleAssignmentPayload">The user role assignment payload.</param>
        /// <param name="context">The context.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="roleService">The role service.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public async Task<Data.Models.UserRoleAssignmentPayload> AssignUserRole(Data.Models.UserRoleAssignmentPayload userRoleAssignmentPayload, [Service] DemoDbContext context, [Service] IUserService userService, [Service] IRoleService roleService, CancellationToken cancellationToken)
        {
            var rolesToAdd = new List<Role>();
            var roleAssociationsToDelete = context.UserRoleScopeAssociations.Where(ur => ur.UserID == userRoleAssignmentPayload.UserID &&
                !userRoleAssignmentPayload.RoleIDs.Contains(ur.RoleID) &&
                ur.CountryID == userRoleAssignmentPayload.CountryID &&
                ur.RetailerID == userRoleAssignmentPayload.RetailerID &&
                ur.ScenarioID == userRoleAssignmentPayload.ScenarioID).ToList();

            var user = userService.GetById(userRoleAssignmentPayload.UserID, true);
            foreach (var roleId in userRoleAssignmentPayload.RoleIDs)
            {
                var availableAssociation = context.UserRoleScopeAssociations.FirstOrDefault(ur => ur.UserID == userRoleAssignmentPayload.UserID &&
                ur.RoleID == roleId &&
                ur.CountryID == userRoleAssignmentPayload.CountryID &&
                ur.RetailerID == userRoleAssignmentPayload.RetailerID &&
                ur.ScenarioID == userRoleAssignmentPayload.ScenarioID);

                if (availableAssociation == null)
                {
                    context.UserRoleScopeAssociations.Add(new Data.Entities.UserRoleScopeAssociation
                    {
                        UserID = userRoleAssignmentPayload.UserID,
                        RoleID = roleId,
                        CountryID = userRoleAssignmentPayload.CountryID,
                        RetailerID = userRoleAssignmentPayload.RetailerID,
                        ScenarioID = userRoleAssignmentPayload.ScenarioID,
                    });
                }

                var role = roleService.GetById(roleId);
                rolesToAdd.Add(role);
            }

            if (roleAssociationsToDelete != null && roleAssociationsToDelete.Count > 0)
            {
                context.UserRoleScopeAssociations.RemoveRange(roleAssociationsToDelete);
            }

            user.Roles = rolesToAdd;

            userService.Update(user);
            userService.Commit();

            await context.SaveChangesAsync();
            //await eventSender.SendAsync(nameof(EventSubscription.EventAdded), addEventPayload, cancellationToken);
            return userRoleAssignmentPayload;
        }
    }
}
