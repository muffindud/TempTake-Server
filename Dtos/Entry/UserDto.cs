
using System.ComponentModel.DataAnnotations;

namespace TempTake_Server.Dtos.Entry
{
    public class UserDto
    {
        [Required]
        public required string TelegramId { get; set; }
        public string TelegramUsername { get; set; }
    }
}