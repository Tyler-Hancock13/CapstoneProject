using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Model.Entities
{
    /// <summary>
    /// A Department within the company.
    /// </summary>
    public class Department : Base
    {
        /// <summary>
        /// The Departments unique identifier
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the Department
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "The Name field must be under 20 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// A brief description of the department
        /// </summary>
        [Required]
        [StringLength(255, ErrorMessage = "The Description field must be under 255 characters.")]
        public string Description { get; set; }

        /// <summary>
        /// The Date the department becomes or became effective
        /// </summary>
        [Display(Name = "Invocation Date")]
        public DateTime InvocationDate { get; set; }

        /// <summary>
        /// The version of the record, used for concurrency
        /// </summary>
        public byte[] Version { get; set; }
    }
}
