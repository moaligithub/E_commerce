using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        [Display(Name = "FirstName")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        [Display(Name = "LastName")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        // Relational Properties
        public City City { get; set; }
        public AdminProperties AdminProperties { get; set; }
        public UserProperties UserProperties { get; set; }
    }
}
