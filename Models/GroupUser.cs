using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempTake_Server.Models
{
    [Table("group_user")]
    public class GroupUser
    {
        // Primary Key
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("is_admin")]
        [Required]
        public bool IsAdmin { get; set; } = false;
        
        [Column("is_confirmed")]
        [Required]
        public bool IsConfirmed { get; set; } = false;
        
        // Timestamps
        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
        
        // Foreign Keys
        [ForeignKey("group")]
        [Column("group_id")]
        [Required]
        public required int GroupId { get; set; }
        public Group? Group { get; set; }
        
        [ForeignKey("user")]
        [Column("user_id")]
        [Required]
        public required int UserId { get; set; }
        public User? User { get; set; }
    }
}