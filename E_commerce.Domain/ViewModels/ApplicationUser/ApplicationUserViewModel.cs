using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.ViewModels.ApplicationUser
{
    public class AdminManagerViewModel
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
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email*")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [Phone]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string CityName { get; set; }
    }
}
