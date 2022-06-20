// <copyright file="EventAuthType.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILock.Core.GraphQL.Demo.Data.AuthTypes
{
    using ILock.Core.GraphQL.Demo.Data.Entities;

    /// <summary>
    /// Event AuthType, provides virtual authorization methods for Event Entity
    /// </summary>
    public class EventAuthType : ObjectType<EventEntity>
    {
        /// <summary>
        /// Configures the Authorize descriptor.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<EventEntity> descriptor)
        {
            descriptor.Authorize(new[] { "Super-Admin" });
        }
    }
}
