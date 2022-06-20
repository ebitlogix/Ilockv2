// <copyright file="AuthObjectType`1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.Services;
using ILock.Core.Services.Abstractions;

namespace ILock.Core.GraphQL.Extensions
{

    /// <summary>
    /// The auth object type or type T.
    /// </summary>
    public static class AuthObjectTypeExtensions
    {
        //private readonly string permissionName;
        //private readonly List<string> accessLevels;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="AuthObjectType{T}"/> class.
        ///// </summary>
        //public AuthObjectType()
        //{
        //    var pName = this.GetType().GetCustomAttributes(false).OfType<TypePermissionNameAttribute>().FirstOrDefault();
        //    if (pName != null)
        //    {
        //        this.permissionName = pName.EntityName;
        //        this.accessLevels = pName.AccessTypes;
        //    }
        //}

        /// <summary>
        /// Configures the authorization.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="permissionName">The permission name.</param>
        /// <param name="accessLevels">The access levels.</param>
        /// <returns>An IObjectFieldDescriptor.</returns>
        public static IObjectFieldDescriptor ConfigureAuthorization(this IObjectFieldDescriptor descriptor, string permissionName, params string[] accessLevels)
        {
            var service = DependencyInjectionServiceFactory.Default.GetService<IPermissionService>();
            var roles = service.GetRolesAssociatedWithPermission(permissionName, accessLevels).Select(r => r.Name);
            return descriptor.Authorize(roles.ToArray());
        }
    }
}
