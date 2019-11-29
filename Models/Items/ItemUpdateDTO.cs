using System.ComponentModel.DataAnnotations;

namespace POETestBot.Models
{
    public class ItemUpdateDTO : DTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public (string,int) price { get; set; }
    }
}