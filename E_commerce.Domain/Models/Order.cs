using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Total")]
        public decimal Total { get; set; }
        
        [Required]
        [Display(Name = "Date Order")]
        [DataType(DataType.DateTime)]
        public DateTime DateOrder { get; set; }
        
        [Required]
        [MaxLength(150)]
        [Display(Name = "Address")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        
        [Required]
        [Display(Name = "Confirmation")]
        public bool Confirmation { get; set; }

        // ForeignKey

        [Required]
        public Guid CityId { get; set; }

        // Relational Properties
        public ICollection<Cart> OrderDetails { get; set; }
        public UserProperties UserProperties { get; set; }

        [ForeignKey(nameof(CityId))]
        public City City { get; set; }
        public Country Country { get; set; }
    }
}
