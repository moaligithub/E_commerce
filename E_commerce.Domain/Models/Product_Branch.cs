using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class Product_Branch
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Date Time")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        // ForeignKey

        [Required]
        public int ProductId { get; set; }

        [Required]
        public Guid BranchId { get; set; }

        // Relational Properties

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [ForeignKey(nameof(BranchId))]
        public Branch Branch { get; set; }
    }
}