// <copyright file="IBaseEntityService.cs" company="Retail Insight Ltd">
// Copyright (c) Retail Insight Ltd. All rights reserved.
// </copyright>

namespace ILock.Core.Services.Abstractions
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    /// <summary>
    /// Base Entity Interface for Crud operations. Aggregates with Readonly Entity Interface
    /// </summary>
    /// <typeparam name="TEntity">Entity Type</typeparam>
    public interface IBaseEntityService<TEntity> : IReadonlyEntityService<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Adds the Entities.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>An EntityEntry.</returns>
        EntityEntry<TEntity> Add(TEntity entity);


        /// <summary>
        /// Adds the TEntity async.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task.</returns>
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

        /// <summary>
        /// Adds the range of Entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void AddRange(List<TEntity> entities);

        /// <summary>
        /// Commits the Entity changes to Database.
        /// </summary>
        void Commit();

        /// <summary>
        /// Commits the Entity changes to Database asynchronously.
        /// </summary>
        /// <returns>A Task.</returns>
        Task CommitAsync();

        /// <summary>
        /// Commits the the cancellation Token asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        Task CommitAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Removes the Entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>An EntityEntry.</returns>
        EntityEntry<TEntity> Remove(TEntity entity);


        /// <summary>
        /// Removes the Entity along with the CancellationToken.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        Task<EntityEntry<TEntity>> RemoveAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Removes these IEnumerable Entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates the Entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>An EntityEntry.</returns>
        EntityEntry<TEntity> Update(TEntity entity);

        /// <summary>
        /// Updates the Entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task.</returns>
        Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates this list of Entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void UpdateRange(List<TEntity> entities);
    }
}