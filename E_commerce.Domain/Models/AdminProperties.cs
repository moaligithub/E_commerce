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
    public class AdminProperties
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] ProfilePicture { get; set; }

        // ForeignKey
        [Required]
        public string UserId { get; set; }

        // Relational Properties
        public Branch Branch { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<AdminActivity> Activities { get; set; }
    }
}
