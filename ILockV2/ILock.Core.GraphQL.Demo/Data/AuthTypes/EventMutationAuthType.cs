// <copyright file="EventMutationAuthType.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.Data.Constants;
using ILock.Core.GraphQL.Demo.Constants;
using ILock.Core.GraphQL.Extensions;

namespace ILock.Core.GraphQL.Demo.Data.AuthTypes
{
    /// <summary>
    /// Event AuthType, provides virtual authorization methods for Event Entity
    /// </summary>
    [TypePermissionName(nameof(EntityName.Event), nameof(AccessLevel.Full), nameof(AccessLevel.Write))]
    public class EventMutationAuthType
    {
    }
}
