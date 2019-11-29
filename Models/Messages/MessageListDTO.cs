using POETestBot.Entities;
using System.ComponentModel.DataAnnotations;

namespace POETestBot.Models
{
    public class MessageListDTO : DTO
    {
        public int Id { get; set; }
        public string timestamp { get; set; }

        [Required]
        public int senderID { get; set; }
        public User sender { get; set; }
        public Rarity rarity { get; set; }
    }
}