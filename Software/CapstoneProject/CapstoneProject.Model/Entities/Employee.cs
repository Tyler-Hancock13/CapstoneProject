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
    /// Employee Model
    /// </summary>
    public class Employee : Base
    {
        /// <summary>
        /// The Employees Unique 8-Digit System-Assigned ID 
        /// </summary>
        public string ID { get; set; }

        
        /// <summary>
        /// The Employees First Name
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        [StringLength(35, MinimumLength = 2, ErrorMessage = "The First Name field must be between 2 and 35 characters.")]
        public string FirstName { get; set; }

        /// <summary>
        /// The Employees Last Name
        /// </summary>
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(35, MinimumLength = 2, ErrorMessage = "The Last Name field must be between 2 and 35 characters.")]
        public string LastName { get; set; }

        /// <summary>
        /// The Employees Middle Initial
        /// </summary>
        [Display(Name = "Middle Initial")]
        [StringLength(1, ErrorMessage = "The Middle Initial field must be 1 character.")]
        public string MiddleInitial { get; set; }

        public string FullName
        {
            get
            {
                return this.MiddleInitial != null ? $"{this.FirstName} {this.MiddleInitial} {this.LastName}" : $"{this.FirstName} {this.LastName}";
            }
        }

        /// <summary>
        /// The Employees Date of Birth
        /// </summary>
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The Employees Street Address
        /// </summary>
        [Display(Name = "Address")]
        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "The Street Address field must be between 10 and 50 characters.")]
        public string StreetAddress { get; set; }

        /// <summary>
        /// The City in which the Employee resides 
        /// </summary>
        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "The City field must be between 2 and 25 characters.")]
        public string City { get; set; }

        /// <summary>
        /// The Employees Postal Code
        /// </summary>
        [Display(Name = "Postal Code")]
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "The Postal Code field must be 6 characters.")]
        [RegularExpression("([ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ])([0-9][ABCEGHJKLMNPRSTVWXYZ][0-9])", ErrorMessage = "The Postal Code field is invalid.")]
        public string PostalCode { get; set; }

        /// <summary>
        /// The Employees Social Insurance Number
        /// </summary>
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "The SIN field must be 11 characters.")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "The SIN field is invalid.")]
        public string SIN { get; set; }

        /// <summary>
        /// The Date the Employee Joined the Company
        /// </summary>
        [Display(Name = "Seniority Date")]
        public DateTime SeniorityDate { get; set; }

        /// <summary>
        /// The Date the Employee began their current posititon
        /// </summary>
        [Display(Name = "Job Start Date")]
        public DateTime JobStartDate { get; set; }

        /// <summary>
        /// The Employees Work Phone Number
        /// </summary>
        [Display(Name = "Work Phone")]
        [StringLength(17, MinimumLength = 7, ErrorMessage = "The Work Phone field must be between 7 and 17 characters.")]
        [Phone]
        public string WorkPhone { get; set; }

        /// <summary>
        /// The Employees Cell Phone Number
        /// </summary>
        [Display(Name = "Cell Phone")]
        [StringLength(17, MinimumLength = 7, ErrorMessage = "The Cell Phone field must be between 7 and 17 characters.")]
        [Phone]
        public string CellPhone { get; set; }

        /// <summary>
        /// The Employees Email Address
        /// </summary>
        [Display(Name = "Email")]
        [Required]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "The Email field must be between 10 and 255 characters.")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The Employees employment status.
        /// </summary>
        public EmployeeStatus Status { get; set; }

        /// <summary>
        /// The Address of the Employee's Office
        /// </summary>
        [Display(Name = "Office Address")]
        [Required]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "The Office Address field must be between 10 and 60 characters.")]
        public string OfficeAddress { get; set; }

        /// <summary>
        /// The City of the Employee's Office
        /// </summary>
        [Display(Name = "Office City")]
        [Required]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "The Office City field must be between 5 and 25 characters.")]
        public string OfficeCity { get; set; }

        /// <summary>
        /// The Unit Number of the Employee's Office
        /// </summary>
        [Display(Name = "Office Unit")]
        public int OfficeUnit { get; set; }

        /// <summary>
        /// The Date the Employee Retired or was Terminated
        /// </summary>
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The version of the record, used for concurrency
        /// </summary>
        public byte[] Version { get; set; }

        /// <summary>
        /// The ID of the Employees Job
        /// </summary>
        [Display(Name = "Job")]
        [Range(1, int.MaxValue, ErrorMessage = "The Job field is required.")]
        public int JobID { get; set; }

        /// <summary>
        /// The ID of the Employees Supervisor
        /// </summary>
        [Display(Name = "Supervisor")]
        [Range(1, int.MaxValue, ErrorMessage = "The Supervisor field is required.")]
        public string SupervisorID { get; set; }

        /// <summary>
        /// The ID of the Employees Department
        /// </summary>
        [Display(Name = "Department")]
        [Range(1, int.MaxValue, ErrorMessage = "The Department field is required.")]
        public int DepartmentID { get; set; }
    }
}
