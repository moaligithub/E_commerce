using E_commerce.Core.ViewModels.ApplicationUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.ViewModels.User
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

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

        [Display(Name = "PhoneNumber")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string CountryId { get; set; }

        [Required]
        public string CityId { get; set; }        
        public string UserName { get; set; }
    }
}