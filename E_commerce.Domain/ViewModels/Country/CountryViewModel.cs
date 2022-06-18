using E_commerce.Domain.ViewModels.Country;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.ViewModels
{
    public class CountryViewModel : AddCountryViewModel
    {
        public Guid Id { get; set; }
        public int CitiesCount { get; set; }
        public int BranchsCount { get; set; }
    }
}
