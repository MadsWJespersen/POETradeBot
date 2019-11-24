namespace POETestBot.Models
{
    public class UserUpdateDTO : UserCreateDTO
    {
        public int Id { get; set; }
        public string passwordHash { get; set; }
    }
}