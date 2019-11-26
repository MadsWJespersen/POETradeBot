using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POETestBot.Entities
{
    public class Message
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string timestamp { get; set; }
        [Required]
        public int senderID { get; set; }
        public User sentBy { get; set; }
        public string textContent { get; set; }

    }
}
