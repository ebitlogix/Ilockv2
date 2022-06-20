using System.ComponentModel.DataAnnotations;

namespace ILock.Core.Data.Entities
{
    /// <summary>
    /// The scope entity.
    /// </summary>
    public class Scope
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent scope.
        /// </summary>
        public Scope ParentScope { get; set; }
    }
}
