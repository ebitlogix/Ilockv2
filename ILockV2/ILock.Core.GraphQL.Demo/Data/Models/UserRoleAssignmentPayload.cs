using ILock.Core.GraphQL.Demo.Data.Entities;

namespace ILock.Core.GraphQL.Demo.Data.Models
{
    /// <summary>
    /// The user role assignment payload.
    /// </summary>
    public class UserRoleAssignmentPayload
    {
        /// <summary>
        /// Gets or sets the user id to assign role to.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public int[] RoleIDs { get; set; }

        /// <summary>
        /// Gets or sets the country i d.
        /// </summary>
        public int CountryID { get; set; }

        /// <summary>
        /// Gets or sets the retailer i d.
        /// </summary>
        public int RetailerID { get; set; }

        /// <summary>
        /// Gets or sets the scenario i d.
        /// </summary>
        public int ScenarioID { get; set; }
    }
}
