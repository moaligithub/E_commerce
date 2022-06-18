using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class Branch
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        // ForeignKey

        [Required]
        public Guid CountryId { get; set; }

        // Relational Properties

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        public ICollection<Product_Branch> product_Branches { get; set; }
        public ICollection<AdminProperties> AdminProperties { get; set; }
    }
}
