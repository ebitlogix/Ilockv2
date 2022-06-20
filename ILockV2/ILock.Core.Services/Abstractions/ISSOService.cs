// <copyright file="ISSOService.cs" company="Retail Insight Ltd">
// Copyright (c) Retail Insight Ltd. All rights reserved.
// </copyright>

using ILock.Core.Data.Models;

namespace ILock.Core.Services.Abstractions
{
    /// <summary>
    /// The SSO service interface.
    /// </summary>
    public interface ISSOService : IBaseEntityService<SSOAuthToken>
    {
        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A SSOAuthToken.</returns>
        SSOAuthToken GetById(string id);
    }
}