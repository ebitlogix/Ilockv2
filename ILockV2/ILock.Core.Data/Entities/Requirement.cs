namespace ILock.Core.Data.Entities
{
    //public record Requirement(int ID, string Name, string Type, string Value, int PolicyID, AccessLevel Access, Policy Policy);
    /// <summary>
    /// The requirement.
    /// </summary>
    public class Requirement
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the access.
        /// </summary>
        public string Access { get; set; }

        /// <summary>
        /// Gets or sets the policy.
        /// </summary>
        public Policy Policy { get; set; }
    }
}
