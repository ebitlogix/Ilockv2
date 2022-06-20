namespace ILock.Core.Data.Models
{
    /// <summary>
    /// The role permission assignment payload.
    /// </summary>
    public class RolePermissionAssignmentPayload : ILockSchemaType
    {
        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        public string? RoleName { get; set; }
        /// <summary>
        /// Gets or sets the feature i ds.
        /// </summary>
        public List<int> FeatureIDs { get; set; }

        /// <summary>
        /// Gets or sets the access types.
        /// </summary>
        public string[] AccessTypes { get; set; }
    }

}
