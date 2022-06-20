// <copyright file="RoleQueryExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.Data.Constants;
using ILock.Core.GraphQL.Extensions.Constants;
using ILock.Core.GraphQL.Extensions.Queries;
using ILock.Core.GraphQL.Extensions.Types;

namespace ILock.Core.GraphQL.Extensions.QueryExtensions
{
    /// <summary>
    /// The role query extension.
    /// </summary>
    public class RoleQueryExtension : ObjectTypeExtension<Query>, ILockAuthQueryType
    {
        private readonly string permissionName;
        private readonly List<string> accessLevels;


        /// <summary>
        /// Initializes a new instance of the <see cref="RoleQueryExtension"/> class.
        /// </summary>
        public RoleQueryExtension()
        {
            this.permissionName = nameof(EntityName.Role);
            this.accessLevels = new List<string>
                {
                    nameof(AccessLevel.Full), nameof(AccessLevel.Read)
                };
        }

        /// <summary>
        /// Configures the query descripto.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(nameof(RoleQueryResolver.GetRoles))
                .ResolveWith<RoleQueryResolver>(_ => _.GetRoles(default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());

            descriptor.Field(nameof(RoleQueryResolver.GetRolesById))
                .Argument("id", a => a.Type<NonNullType<IntType>>())
            .ResolveWith<RoleQueryResolver>(_ => _.GetRolesById(default, default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());
        }
    }
}
