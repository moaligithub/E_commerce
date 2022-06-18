using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class Product
    { 
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        
        [Required]
        [Display(Name = "Discount")]
        public bool Discount { get; set; }

        [Display(Name = "Price After Discount")]
        public decimal PriceAfterDescount { get; set; }
        
        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        // Foreign Key

        [Required]
        public Guid CategoryId { get; set; }

        // Relational Properties
        public ICollection<Cart> OrderDetails { get; set; }
        public ICollection<Product_Branch> product_Branches { get; set; }
        public ICollection<Purchases> Purchases { get; set; }
        public ICollection<Product_Images> ProductImages { get; set; }
        
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
