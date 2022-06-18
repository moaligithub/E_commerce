using E_commerce.Core.ViewModels.ApplicationUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.ViewModels.Admin
{
    public class AdminsIndexViewModel
    {
        public string FullName { get; set; }
        public byte[] ProfilePicture { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email*")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
