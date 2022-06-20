namespace ILock.Core.Data.Helpers
{
    /// <summary>
    /// The DbContextSchema class to set custom schema.
    /// </summary>
    public class DbContextSchema : IDbContextSchema
    {
        /// <summary>
        /// Gets the schema.
        /// </summary>
        public string Schema { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextSchema"/> class.
        /// </summary>
        /// <param name="schema">The schema.</param>
        public DbContextSchema(string schema)
        {
            Schema = schema;
        }
    }
}
