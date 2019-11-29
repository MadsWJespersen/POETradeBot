using System.ComponentModel.DataAnnotations;

namespace POETestBot.Models
{
    public class UserCreateDTO : DTO
    {
        [Required]
        public string Name { get; set; }


        
    }
}