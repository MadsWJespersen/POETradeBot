using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POETestBot.Models
{
    public class ItemBuyDTO : DTO
    {
        public string username { get; set; }
        public string itemname { get; set; }
        public (string,int) price { get; set; }
        public (string,int,int) position { get; set; }
    }
}
