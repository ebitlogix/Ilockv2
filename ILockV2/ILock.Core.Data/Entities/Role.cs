using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILock.Core.Data.Entities
{
    /// <summary>
    /// Role Entity
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
            Users = new List<User>();
            Policies = new List<Policy>();
        }

        /// <summary>
        /// Gets or Sets ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Permissions Navigation Property
        /// Permissions which are associated with this role
        /// </summary>
        public List<Permission> Permissions { get; set; }

        /// <summary>
        /// Gets or Sets Users navigation property
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// Gets or Sets Policies Navgiation proeperty. Not using right now
        /// </summary>
        public List<Policy> Policies { get; set; }

    }
}
