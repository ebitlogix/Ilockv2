using ILock.Core.Data.Constants;
using ILock.Core.GraphQL.Extensions.Constants;
using ILock.Core.GraphQL.Extensions.Queries;
using ILock.Core.GraphQL.Extensions.Types;

namespace ILock.Core.GraphQL.Extensions.QueryExtensions
{
    /// <summary>
    /// The permission query extension.
    /// </summary>
    public class PermissionQueryExtension : ObjectTypeExtension<Query>, ILockAuthQueryType
    {
        private readonly string permissionName;
        private readonly List<string> accessLevels;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionQueryExtension"/> class.
        /// </summary>
        public PermissionQueryExtension()
        {
            this.permissionName = nameof(EntityName.Permission);
            this.accessLevels = new List<string>
                {
                    nameof(AccessLevel.Full), nameof(AccessLevel.Read)
                };
        }

        /// <summary>
        /// Configures the query descriptors.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(nameof(PermissionQueryResolver.GetPermissions))
                .ResolveWith<PermissionQueryResolver>(_ => _.GetPermissions(default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());

            descriptor.Field(nameof(PermissionQueryResolver.GetPermissionsByRoleId))
                .Argument("id", a => a.Type<NonNullType<IntType>>())
            .ResolveWith<PermissionQueryResolver>(_ => _.GetPermissionsByRoleId(default, default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());
        }
    }
}
