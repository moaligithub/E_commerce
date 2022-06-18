using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class UserProperties
    {
        [Key]
        public Guid Id { get; set; }

        // ForeignKey
        public string UserId { get; set; }

        // Relational Properties
        public ICollection<Cart> Carts { get; set; }
        public ICollection<WishList> wishLists { get; set; } 
        public ICollection<Order> Orders { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
