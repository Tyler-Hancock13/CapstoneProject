using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Model.Entities
{
    public class PurchaseOrder : Base
    {
        /// <summary>
        /// The Purchase Order ID
        /// </summary>
        public int PONumber { get; set; }

        /// <summary>
        /// The Status of the Purchase Order
        /// </summary>
        public RequestStatus Status { get; set; }

        /// <summary>
        /// The List of Items in the Purchase Order
        /// </summary>
        public List<PurchaseOrderItem> Items { get; set; }

        /// <summary>
        /// The Employee ID the Purchase Order belongs to
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// The name of the Employee the Purchase Order belongs to
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// The Supervisors ID for the Purchase Order
        /// </summary>
        public string SupervisorId { get; set; }

        /// <summary>
        /// The Department ID the Purchase Order belongs to
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// The Name of the Department the Purchase Order belongs to
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// The Subtotal of the Purchase Order
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// The Sales Tax of the Purchase Order
        /// </summary>
        public decimal SalesTax { get; set; }

        /// <summary>
        /// The Total Cost of the Purchase Order
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// The Date the Purchase Order was created
        /// </summary>
        public DateTime DateCreated { get; set; }

        //private byte[] Version { get; set; }
    }
}
