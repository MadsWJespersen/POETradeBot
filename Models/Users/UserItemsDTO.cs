using POETestBot.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POETestBot.Models
{
    class UserItemsDTO
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        [StringLength(23)]
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
