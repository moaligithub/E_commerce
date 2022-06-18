using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class Purchases
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
       
        [Required]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
        
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        // ForeignKey

        [Required]
        public int ProductId { get; set; }

        // Relational Properties

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
