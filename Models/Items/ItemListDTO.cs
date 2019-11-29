using POETestBot.Entities;
using System.ComponentModel.DataAnnotations;

namespace POETestBot.Models
{
    public class ItemListDTO : DTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public int ownerID { get; set; }
        public User owner { get; set; }
        public Rarity rarity { get; set; }
    }
}