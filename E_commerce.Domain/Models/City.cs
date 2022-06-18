using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class City
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        // ForeignKey
       
        [Required]
        public Guid CountryId { get; set; }

        // Relational Properties
        public ICollection<ApplicationUser> applicationUsers { get; set; }
        
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
    }
}
