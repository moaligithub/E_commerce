
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.ViewModels.Admin
{
    public class AdminActivityStatsViewModel
    {
        public List<int> AddedStats { get; set; }
        public List<int> UpDateStats { get; set; }
        public List<int> DeleteStats { get; set; }
        public int RelativeStatsForAdded { get; set; }
        public int RelativeStatsForUpDated { get; set; }
        public int RelativeStatsForDeleted { get; set; }
    }
}
