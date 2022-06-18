using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.ViewModels.Branch
{
    public class AddBranchViewModel
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
    }
}
