using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILock.Core.Data.Models
{
    public class UserLoginHistory
    {
        /// <summary>
        /// Gets or sets the i d.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the issued at.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        public string IpAddress { get; set; }
    }
}
