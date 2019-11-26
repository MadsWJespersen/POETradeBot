using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POETestBot.Entities
{
    public class User
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(23)]
        public string Name { get; set; }
        public string passwordHash { get; set; }

        public Dictionary<string, int> balance { get; set; }
        public ICollection<Message> Messages { get; set; }

        public IList<Item> Items { get; set; }

    }
}
