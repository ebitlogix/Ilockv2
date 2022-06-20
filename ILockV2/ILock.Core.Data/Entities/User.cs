using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ILock.Core.Data.Entities
{
    /// <summary>
    /// The user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            Roles = new List<Role>();
            Policies = new List<Policy>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int ID { get; set; }
        
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }
        
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether in active.
        /// </summary>
        public bool InActive { get; set; }
        
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public List<Role>? Roles { get; set; }
        
        /// <summary>
        /// Gets or sets the policies.
        /// </summary>
        public List<Policy>? Policies { get; set; }
        
        /// <summary>
        /// Gets or sets the resources.
        /// </summary>
        public List<Resource>? Resources { get; set; }
        
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [Column(TypeName = "jsonb")]
        public string Content { get; set; }
    }
}
