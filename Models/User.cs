using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempTake_Server.Models
{
    [Table("user")]
    public class User
    {
        // Primary Key
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("telegram_user_id", TypeName = "varchar(32)")]
        [Required]
        public required string TelegramUserId { get; set; }
        
        [Column("telegram_username", TypeName = "varchar(32)")]
        [Required]
        public required string TelegramUsername { get; set; }
        
        // Timestamps
        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}