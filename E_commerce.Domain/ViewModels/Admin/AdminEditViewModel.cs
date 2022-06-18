using E_commerce.Core.ViewModels.ApplicationUser;
using E_commerce.Domain.ViewModels.AdminManager;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.ViewModels.Admin
{
    public class AdminEditViewModel : EditAdminManagerViewModel
    {
        [Required]
        public Guid BranchId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email*")]
        public string Email { get; set; }
    }
}
