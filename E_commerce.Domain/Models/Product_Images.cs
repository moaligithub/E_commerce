using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class Product_Images
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        [DataType(DataType.Text)]
        public string ImageUrl { get; set; }

        // ForeignKey

        [Required]
        public int ProductId { get; set; }

        // Relational Properties

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}