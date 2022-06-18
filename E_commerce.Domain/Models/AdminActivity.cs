using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Models
{
    public class AdminActivity
    {
        [Key]
        public Guid Id { get; set; }
        public string TypeActive { get; set; }
        public DateTime DateTime { get; set; }
        public string Details { get; set; }

        // Foreign Key
        public Guid AdminId { get; set; }
        
        // Relational Properties
        [ForeignKey(nameof(AdminId))]
        public AdminProperties AdminProperties { get; set; }
    }
}
