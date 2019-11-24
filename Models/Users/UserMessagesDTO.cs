using POETestBot.Entities;
using System.Collections.Generic;

namespace POETestBot.Models
{
    public class UserMessagesDTO
    {
        public ICollection<Message> Messages { get; set; }
    }
}