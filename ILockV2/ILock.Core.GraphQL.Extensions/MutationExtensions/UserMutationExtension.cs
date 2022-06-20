using ILock.Core.Data.Constants;
using ILock.Core.Data.Entities;
using ILock.Core.GraphQL.Extensions.Constants;
using ILock.Core.GraphQL.Extensions.Mutations;
using ILock.Core.GraphQL.Extensions.Types;

namespace ILock.Core.GraphQL.Extensions.MutationExtensions
{
    /// <summary>
    /// The user mutation extension.
    /// </summary>
    public class UserMutationExtension : ObjectTypeExtension<Mutation>, ILockAuthMutationType
    {
        private readonly string permissionName;
        private readonly List<string> accessLevels;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMutationExtension"/> class.
        /// </summary>
        public UserMutationExtension()
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
            descriptor.Field(nameof(UserMutation.AddUser))
                 .Argument("addUserPayload", a => a.Type<ILock.Core.GraphQL.Extensions.Arguments.UserArgument>())
                .ResolveWith<UserMutation>(_ => _.AddUser(default, default, default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());

            descriptor.Field(nameof(UserMutation.UpdateUserAsync))
                 .Argument("updateUserPayload", a => a.Type<ILock.Core.GraphQL.Extensions.Arguments.UserArgument>())
                .ResolveWith<UserMutation>(_ => _.UpdateUserAsync(default, default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());
        }
    }
}
