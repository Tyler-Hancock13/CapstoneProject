using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Model.Entities
{
    /// <summary>
    /// A User of the sysem, used to login.
    /// </summary>
    public class User : Base
    {
        /// <summary>
        /// The Users unique identifier
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The ID of the Emplyee that owns the account
        /// </summary>
        public string EmployeeID { get; set; }

        /// <summary>
        /// The Users upassword
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// The Users Role at the company
        /// </summary>
        [Required]
        public Role Role { get; set; }
    }
}
