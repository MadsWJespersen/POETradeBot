﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POETestBot.Models
{
    public class ItemBuyDTO : DTO
    {
        [Required]
        public string username { get; set; }
    }
}
