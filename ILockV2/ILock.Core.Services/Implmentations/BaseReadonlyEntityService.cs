// <copyright file="BaseReadonlyEntityService.cs" company="Retail Insight Ltd">
// Copyright (c) Retail Insight Ltd. All rights reserved.
// </copyright>

using ILock.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ILock.Core.Services.Implmentations
{
    /// <summary>
    /// Base Entity Service with all Readonly operations
    /// </summary>
    public class BaseReadonlyEntityService<TEntity> : IReadonlyEntityService<TEntity> 
        where TEntity : class
    {
        private readonly DbSet<TEntity> entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseReadonlyEntityService{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public BaseReadonlyEntityService(DbContext dbContext)
        {
            this.entities = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Gets the Entities by keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns>A TEntity.</returns>
        public TEntity GetByKeys(params object[] keys) => entities.Find(keys);

        /// <summary>
        /// Gets the Entity by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A TEntity.</returns>
        public TEntity GetById(int id) => entities.Find(id);

        /// <summary>
        /// Gets the Entity by id asynchronously.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        public Task<TEntity> GetByIdAsync(int id) => Task.FromResult(GetById(id));

        /// <summary>
        /// Implements GetAll method
        /// </summary>
        /// <returns>list of objects</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return this.entities.ToList();
        }

        /// <summary>
        /// Execute the Where expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A list of TEntities.</returns>
        protected IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return this.entities.Where(expression).AsEnumerable();
        }

        /// <summary>
        /// Executes a FirstOrDefault Expression
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>any</returns>
        protected TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return this.entities.FirstOrDefault(expression);
        }
    }
}
