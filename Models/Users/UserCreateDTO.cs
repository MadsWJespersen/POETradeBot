using System.ComponentModel.DataAnnotations;

namespace POETestBot.Models
{
    public class UserCreateDTO
    {
        [Required]
        public string Name { get; set; }
        
    }
}