using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Model.Entities
{
    public class PurchaseOrderItem : Base
    {
        /// <summary>
        /// The ID of the Purchase Order Item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the Purchase Order Item
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The Description of the Purchase Order Item
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// The Quantity of the Purchase Order Item
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "The Quantity field is required.")]
        public int Quantity { get; set; }

        /// <summary>
        /// The Price of the Purchase Order Item
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "The Price field is required.")]
        public decimal Price { get; set; }

        /// <summary>
        /// The Justification for the Purchase Order Item
        /// </summary>
        [Required]
        public string Justification { get; set; }

        /// <summary>
        /// The Location to get Purchase Order Item
        /// </summary>
        [Required]
        public string Location { get; set; }

        /// <summary>
        /// The Status of the Purchase Order Item
        /// </summary>
        [Required]
        public ItemStatus Status { get; set; }

        /// <summary>
        /// The Subtotal of the Purchase Order Item
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "The Subtotal field is required.")]
        public decimal Subtotal { get; set; }

        /// <summary>
        /// The Number of the Purchase Order the Item belongs to
        /// </summary>
        public int PONumber { get; set; }

        /// <summary>
        /// The Reason for Denying or Editing an Item
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// The Timestamp from when the Item was Updated
        /// </summary>
        public byte[] Timestamp { get; set; }
    }
}
