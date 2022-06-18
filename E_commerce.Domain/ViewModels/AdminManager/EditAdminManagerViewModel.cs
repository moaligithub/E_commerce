using E_commerce.Domain.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.ViewModels.AdminManager
{
    public class EditAdminManagerViewModel : UserEditViewModel
    {
        public byte[] ProfilePicture { get; set; }

        [Display(Name = "Upload Profile Picture")]
        public IFormFile File { get; set; }
    }
}
