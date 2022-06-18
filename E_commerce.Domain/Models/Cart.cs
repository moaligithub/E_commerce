using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cuantity")]
        public int Cuantity { get; set; }
        
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        // ForeignKey

        [Required]
        public int ProductId { get; set; }
        
        [Required]
        public Guid UserId { get; set; }

        // Relational Properties

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public UserProperties UserProperties { get; set; }
        public Order Order { get; set; }
    }
}
