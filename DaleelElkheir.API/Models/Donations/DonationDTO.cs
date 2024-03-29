﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Donations
{
    public class DonationDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Contact { get; set; }
    }
}