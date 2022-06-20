namespace ILock.Core.Data.Entities
{
    /// <summary>
    /// The resource.
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// Gets or sets the i d.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User User { get; set; }
    }
}
