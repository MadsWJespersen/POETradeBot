using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POETestBot.Entities
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(23)]
        public string Name { get; set; }
        public string passwordHash { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}
