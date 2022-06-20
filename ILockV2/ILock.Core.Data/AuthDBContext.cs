using ILock.Core.Data.Entities;
using ILock.Core.Data.Helpers;
using ILock.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ILock.Core.Data
{
    /// <summary>
    /// The AuthDBcontext.
    /// </summary>
    public class AuthDBContext : DbContext
    {
        private readonly string schema;

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public DbSet<Role> Roles { get; set; }
        /// <summary>
        /// Gets or sets the policies.
        /// </summary>
        public DbSet<Policy> Policies { get; set; }
        /// <summary>
        /// Gets or sets the features.
        /// </summary>
        public DbSet<Feature> Features { get; set; }
        /// <summary>
        /// Gets or sets the requirements.
        /// </summary>
        public DbSet<Requirement> Requirements { get; set; }
        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the feature attributes.
        /// </summary>
        public DbSet<FeatureAttribute> FeatureAttributes { get; set; }
        /// <summary>
        /// Gets or sets the feature associations.
        /// </summary>
        public DbSet<FeatureAssociation> FeatureAssociations { get; set; }
        /// <summary>
        /// Gets or sets the auth tokens.
        /// </summary>
        public DbSet<AuthToken> AuthTokens { get; set; }
        /// <summary>
        /// Gets or sets the SSO auth tokens.
        /// </summary>
        public DbSet<SSOAuthToken> SSOAuthTokens { get; set; }
        /// <summary>
        /// Gets or sets the scopes.
        /// </summary>
        public DbSet<Scope> Scopes { get; set; }

        /// <summary>
        /// Gets or sets the auth tokens.
        /// </summary>
        public DbSet<UserLoginHistory> UserLoginHistories { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthDBContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="defaultSchema">The default schema.</param>
        public AuthDBContext(DbContextOptions<AuthDBContext> options, IDbContextSchema defaultSchema = null) : base(options)
        {
            this.schema = defaultSchema?.Schema ?? "security";
        }

        /// <summary>
        /// Ons the model creating.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema);

            base.OnModelCreating(modelBuilder);
        }
    }
}
