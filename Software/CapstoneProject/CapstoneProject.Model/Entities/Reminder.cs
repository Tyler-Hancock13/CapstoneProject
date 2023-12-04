using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Model.Entities
{
    /// <summary>
    /// Reminder Model
    /// </summary>
    public class Reminder
    {
        /// <summary>
        /// The ID of the Reminder.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The Date the Reminder was sent.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Empty Constructor.
        /// </summary>
        public Reminder() { }

        /// <summary>
        /// Constructor that initializes the Date property.
        /// </summary>
        /// <param name="date">The date the Reminder was sent.</param>
        public Reminder(DateTime date)
        {
            Date = date;
        }
    }
}
