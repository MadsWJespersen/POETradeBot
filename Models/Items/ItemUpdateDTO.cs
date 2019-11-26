using System.ComponentModel.DataAnnotations;

namespace POETestBot.Models
{
    public class ItemUpdateDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public (string,int) price { get; set; }
    }
}