// <copyright file="DemoDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.GraphQL.Demo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ILock.Core.GraphQL.Demo.Data
{
    /// <summary>
    /// The demo db context.
    /// </summary>
    public class DemoDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DemoDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        public DbSet<Entities.EventEntity> Events { get; set; }

        /// <summary>
        /// Gets or sets Financials
        /// </summary>
        public DbSet<Financial> Financials { get; set; }

        /// <summary>
        /// Gets or sets the retailers.
        /// </summary>
        public DbSet<Retailer> Retailers { get; set; }

        /// <summary>
        /// Gets or sets the scenarios.
        /// </summary>
        public DbSet<Scenario> Scenarios { get; set; }

        /// <summary>
        /// Gets or sets the countries.
        /// </summary>
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Gets or sets the user role scope associations.
        /// </summary>
        /// 

        public DbSet<UserRoleScopeAssociation> UserRoleScopeAssociations { get; set; }
    }
}
