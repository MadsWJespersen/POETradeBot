using System.ComponentModel.DataAnnotations;

namespace POETestBot.Entities
{
    public class Item
    {
        //[IC] [name] [currency,amount]
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(300)]
        public string name { get; set; }
        [Required]
        public (string, int, int) position { get; set; }
        [Required]
        public int ownerID { get; set; }
        public User owner { get; set; }
        [Required]
        public Rarity rarity { get; set; }
        [Required]
        public string itemRawString { get; set; }
        [Required]
        public (string,int) price { get; set; }
    }
}