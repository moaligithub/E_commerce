using E_commerce.Core.ViewModels.AdminActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.ViewModels.Admin
{
    public class AdminDetailsViewModel : AdminsIndexViewModel
    {
        public string Id { get; set; }
        public string Branch { get; set; }
        public string City { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public AdminActivityStatsViewModel AdminActivityStats { get; set; }
        public List<AdminActivityViewModel> AdminActivities { get; set; }
    }
}
