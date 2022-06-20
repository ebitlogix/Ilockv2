// <copyright file="BaseEntityService.cs" company="Retail Insight Ltd">
// Copyright (c) Retail Insight Ltd. All rights reserved.
// </copyright>

using ILock.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ILock.Core.Services.Implmentations
{

    /// <summary>
    /// The base entity service.
    /// </summary>
    public class BaseEntityService<TEntity> : BaseReadonlyEntityService<TEntity>, IBaseEntityService<TEntity> 
        where TEntity : class
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> entities;


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntityService{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public BaseEntityService(DbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
            this.entities = dbContext.Set<TEntity>();
        }


        /// <summary>
        /// Adds the Entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>An EntityEntry.</returns>
        public virtual EntityEntry<TEntity> Add(TEntity entity) => entities.Add(entity);


        /// <summary>
        /// Removes the Entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>An EntityEntry.</returns>
        public EntityEntry<TEntity> Remove(TEntity entity) => entities.Remove(entity);


        /// <summary>
        /// Updates the Entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>An EntityEntry.</returns>
        public virtual EntityEntry<TEntity> Update(TEntity entity) => entities.Update(entity);


        /// <summary>
        /// Adds the Entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task.</returns>
        public virtual Task<EntityEntry<TEntity>> AddAsync(TEntity entity) => Task.FromResult(Add(entity));

        /// <summary>
        /// Removes the Entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task<EntityEntry<TEntity>> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<EntityEntry<TEntity>>(cancellationToken);
            }
            return Task.FromResult(Remove(entity));
        }

        /// <summary>
        /// Updates the Entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task.</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity) => Task.FromResult(Update(entity));

        /// <summary>
        /// Adds the list of Entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void AddRange(List<TEntity> entities)
        {
            this.entities.AddRange(entities);
        }


        /// <summary>
        /// Removes the list of entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void RemoveRange(IEnumerable<TEntity> entities) => this.entities.RemoveRange(entities);

        /// <summary>
        /// Updates the list of Entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void UpdateRange(List<TEntity> entities)
        {
            this.entities.UpdateRange(entities);
        }

        /// <summary>
        /// Commit Changes to Database
        /// </summary>
        public void Commit() => dbContext.SaveChanges();

        /// <summary>
        /// Commits the changes along with cancellationToken asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task CommitAsync(CancellationToken cancellationToken) => dbContext.SaveChangesAsync(cancellationToken);

        /// <summary>
        /// Commits the changes to database asynchronously.
        /// </summary>
        /// <returns>A Task.</returns>
        public Task CommitAsync() => dbContext.SaveChangesAsync();

    }
}
