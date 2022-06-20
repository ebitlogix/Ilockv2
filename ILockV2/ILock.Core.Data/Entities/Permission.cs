namespace ILock.Core.Data.Entities
{
    /// <summary>
    /// Permission model which contains permissions of a feature that will be assigned to a role(s).
    /// Role has multiple features permissions.
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        public Permission()
        {
            this.Roles = new HashSet<Role>();
        }
        /// <summary>
        /// Gets or Sets Permission ID
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Gets or Sets User ID, currently not using
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// Gets or Sets GroupID, currently not using.
        /// </summary>
        public int? GroupID { get; set; }

        /// <summary>
        /// Gets or Sets FeatureID, for which this Permission is.
        /// </summary>
        public int FeatureID { get; set; }

        /// <summary>
        /// Gets or Sets Access level of this permission, e.g. full, read, none
        /// </summary>
        public string? Access { get; set; }

        /// <summary>
        /// Gets or Sets Permission Type, feature, query, mutation etc. Not using right now
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or Sets Value. Not using right now
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Gets or Sets Roles Navigation property
        /// </summary>
        public ICollection<Role>? Roles { get; set; }

        /// <summary>
        /// Gets or Sets User Navigation Property
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or Sets Feature navigation property
        /// </summary>
        public Feature Feature { get; set; }
    }
}
