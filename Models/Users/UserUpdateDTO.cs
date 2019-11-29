namespace POETestBot.Models
{
    public class UserUpdateDTO : DTO
    {
        public int ID { get; set; }
        public string passwordHash { get; set; }
    }
}