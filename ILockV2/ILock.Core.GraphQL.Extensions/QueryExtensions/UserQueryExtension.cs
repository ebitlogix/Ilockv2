using ILock.Core.Data.Constants;
using ILock.Core.GraphQL.Extensions.Constants;
using ILock.Core.GraphQL.Extensions.Queries;
using ILock.Core.GraphQL.Extensions.Types;

namespace ILock.Core.GraphQL.Extensions.QueryExtensions
{
    /// <summary>
    /// The user query extension.
    /// </summary>
    public class UserQueryExtension : ObjectTypeExtension<Query>, ILockAuthQueryType
    {
        private readonly string permissionName;
        private readonly List<string> accessLevels;


        /// <summary>
        /// Initializes a new instance of the <see cref="UserQueryExtension"/> class.
        /// </summary>
        public UserQueryExtension()
        {
            this.permissionName = nameof(EntityName.Feature);
            this.accessLevels = new List<string>
                {
                    nameof(AccessLevel.Full), nameof(AccessLevel.Read)
                };
        }

        /// <summary>
        /// Configures the.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(nameof(UserQueryResolver.GetUsers))
                .ResolveWith<UserQueryResolver>(_ => _.GetUsers(default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());

            descriptor.Field(nameof(UserQueryResolver.GetUserById))
                .Argument("id", a => a.Type<NonNullType<IntType>>())
            .ResolveWith<UserQueryResolver>(_ => _.GetUserById(default, default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());

            descriptor.Field(nameof(UserQueryResolver.GetUsersRoles))
                .Argument("userId", a => a.Type<NonNullType<IntType>>())
            .ResolveWith<UserQueryResolver>(_ => _.GetUsersRoles(default, default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());
        }
    }
}
