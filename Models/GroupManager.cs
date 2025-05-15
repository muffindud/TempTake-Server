using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempTake_Server.Models
{
    [Table("group_manager")]
    public class GroupManager
    {
        // Primary Key
        [Key]
        [Column("id")]
        public int Id { get; set; }

        // Timestamps
        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; } = null;

        // Foreign Keys
        [ForeignKey("group")]
        [Column("group_id")]
        [Required]
        public required int GroupId { get; set; }
        public Group? Group { get; set; }
        
        [ForeignKey("manager")]
        [Column("manager_id")]
        [Required]
        public required int ManagerId { get; set; }
        public Manager? Manager { get; set; }

    }
}