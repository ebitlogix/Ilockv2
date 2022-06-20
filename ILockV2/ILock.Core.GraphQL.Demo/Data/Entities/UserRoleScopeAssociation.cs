using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILock.Core.GraphQL.Demo.Data.Entities
{
    /// <summary>
    /// The user role scope association.
    /// </summary>
    public class UserRoleScopeAssociation
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the role ID.
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Gets or sets the country i d.
        /// </summary>
        public int CountryID { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Gets or sets the retailer i d.
        /// </summary>
        public int RetailerID { get; set; }

        /// <summary>
        /// Gets or sets the retailer.
        /// </summary>
        public Retailer Retailer { get; set; }

        /// <summary>
        /// Gets or sets the scenario i d.
        /// </summary>
        public int ScenarioID { get; set; }

        /// <summary>
        /// Gets or sets the scenario.
        /// </summary>
        public Scenario Scenario { get; set; }
    }
}
