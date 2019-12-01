namespace POETestBot.Models
{
    public class UserUpdateDTO : DTO
    {
        public string Name { get; set; }
        public string passwordHash { get; set; }
    }
}