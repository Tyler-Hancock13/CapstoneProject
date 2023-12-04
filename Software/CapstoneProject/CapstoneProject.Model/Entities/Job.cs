using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Model.Entities
{
    /// <summary>
    /// A Job belongs to a department and is assigned to Employees
    /// </summary>
    public class Job : Base
    {
        /// <summary>
        /// The Jobs unique identifier
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The Name of the Job
        /// </summary>
        public string Name { get; set; }
    }
}
