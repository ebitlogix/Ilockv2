// <copyright file="IReadonlyEntityService.cs" company="Retail Insight Ltd">
// Copyright (c) Retail Insight Ltd. All rights reserved.
// </copyright>

namespace ILock.Core.Services.Abstractions
{
    /// <summary>
    /// Interface for ReadOnlyEntityService.
    /// </summary>
    /// <return>TEntity</return>
    public interface IReadonlyEntityService<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Interface for getting all Entities
        /// </summary>
        /// <returns>TEntity</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A TEntity.</returns>
        TEntity GetById(int id);

        /// <summary>
        /// Gets the Entity by id asynchronously.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task<TEntity> GetByIdAsync(int id);
    }
}