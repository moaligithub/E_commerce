using E_commerce.Domain.ViewModels.Branch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.ViewModels.Branch
{
    public class BranchViewModel : AddBranchViewModel
    {
        public Guid Id { get; set; }
    }
}
