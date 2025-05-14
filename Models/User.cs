using System.ComponentModel.DataAnnotations.Schema;

namespace TempTake_Server.Models
{
    public class User
    {
        // Primary Key
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(32)")]
        public required string TelegramUserId { get; set; }
        
        [Column(TypeName = "varchar(32)")]
        public required string TelegramUsername { get; set; }
        
        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = null;
    }
}