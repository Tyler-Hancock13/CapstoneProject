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
    /// Review Model
    /// </summary>
    public class Review : Base
    {
        /// <summary>
        /// The Reviews Unique Identifier
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The ID of the Employee that the Review is for.
        /// </summary>
        [Required]
        [Display(Name = "Employee")]
        public string EmployeeID { get; set; }

        /// <summary>
        /// The ID of the Supervisor that created the Review.
        /// </summary>
        [Required]
        [Display(Name = "Supervisor")]
        public string SupervisorID { get; set; }

        /// <summary>
        /// The Date of the Review.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// The Rating the Employee recieved on this Review.
        /// </summary>
        [Required]
        public Rating Rating { get; set; }

        /// <summary>
        /// The Detailed Comments for the Review.
        /// </summary>
        [Required]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "The Comment field must be between 5 and 255 characters.")]
        public string Comment { get; set; }
    }
}
