using ILock.Core.Data.Constants;
using ILock.Core.Data.Models;
using ILock.Core.GraphQL.Extensions.Constants;
using ILock.Core.GraphQL.Extensions.Mutations;
using ILock.Core.GraphQL.Extensions.Types;

namespace ILock.Core.GraphQL.Extensions.MutationExtensions
{
    /// <summary>
    /// The permission mutation extension.
    /// </summary>
    public class PermissionMutationExtension : ObjectTypeExtension<Mutation>, ILockAuthMutationType
    {
        private readonly string permissionName;
        private readonly List<string> accessLevels;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionMutationExtension"/> class.
        /// </summary>
        public PermissionMutationExtension()
        {
            this.permissionName = nameof(EntityName.Feature);
            this.accessLevels = new List<string>
                {
                    nameof(AccessLevel.Full), nameof(AccessLevel.Write)
                };
        }

        /// <summary>
        /// Configures the mutation descriptor.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field(nameof(PermissionMutation.AssignPermissionsWithRoleId))
                .Argument("rolePermissionAssignmentPayload", a => a.Type<ILock.Core.GraphQL.Extensions.Arguments.RolePermissionAssignmentArgument>())
                .ResolveWith<PermissionMutation>(_ => _.AssignPermissionsWithRoleId(default, default, default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());
        }
    }
}
