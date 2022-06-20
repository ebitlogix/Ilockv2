// <copyright file="TypePermissionNameAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILock.Core.GraphQL.Extensions
{
    /// <summary>
    /// The type permission name attribute.
    /// </summary
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class TypePermissionNameAttribute : Attribute
    {
        private readonly string entityName;
        private readonly List<string> accessTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypePermissionNameAttribute"/> class.
        /// </summary>
        /// <param name="entityName">The entity name.</param>
        /// <param name="accessTypes">The access types.</param>
        public TypePermissionNameAttribute(string entityName, params string[] accessTypes)
        {
            this.entityName = entityName;
            this.accessTypes = accessTypes.ToList();
        }

        /// <summary>
        /// Gets the entity name.
        /// </summary>
        public string EntityName
        {
            get { return this.entityName; }
        }

        /// <summary>
        /// Gets the access types.
        /// </summary>
        public List<string> AccessTypes
        {
            get { return this.accessTypes; }
        }
    }
}
