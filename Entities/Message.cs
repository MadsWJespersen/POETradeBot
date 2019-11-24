using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POETestBot.Entities
{
    public class Message
    {
        [Required]
        public string timestamp { get; set; }
        [Required]
        public int senderID { get; set; }

        public User SentBy { get; set; }

        public string textContent { get; set; }

    }
}
