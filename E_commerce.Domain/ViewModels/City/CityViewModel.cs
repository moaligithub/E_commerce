﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.ViewModels.City
{
    public class CityViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
