// <copyright file="FinancialQueryAuthType.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.Data.Constants;
using ILock.Core.GraphQL.Demo.Constants;
using ILock.Core.GraphQL.Extensions;
using ILock.Core.GraphQL.Extensions.Queries;

namespace ILock.Core.GraphQL.Demo.Data.AuthTypes
{
    /// <summary>
    /// Event AuthType, provides virtual authorization methods for Event Entity
    /// </summary>
    [TypePermissionName(nameof(EntityName.Financail), nameof(AccessLevel.Full), nameof(AccessLevel.Read))]
    [ExtendObjectType(typeof(Query))]
    public class FinancialQueryAuthType
    {
    }
}
