using POETestBot.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POETestBot.Models
{
    public class UserMessagesDTO : DTO
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        [StringLength(23)]
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}