namespace POETestBot.Models
{
    public class ItemCreateDTO : DTO
    {
        public string itemname { get; set; }
        public string username { get; set; }
        public (string,int) price { get; set; }
    }
}