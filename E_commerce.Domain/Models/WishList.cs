using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class WishList
    {
        [Key]
        public Guid Id { get; set; }

        // ForeignKey
        [Required]
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        
        // Relational Properties
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserProperties UserProperties { get; set; }
    }
}