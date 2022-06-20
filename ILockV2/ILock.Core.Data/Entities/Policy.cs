using System.ComponentModel.DataAnnotations;
namespace ILock.Core.Data.Entities
{
    /// <summary>
    /// The policy.
    /// </summary>
    public class Policy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Policy"/> class.
        /// </summary>
        public Policy()
        {
            Roles = new List<Role>();
        }

        /// <summary>
        /// Gets or sets the i d.
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether is system is true or false.
        /// </summary>
        public bool IsSystem { get; set; }
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public List<Role> Roles { get; set; }

    }
}
