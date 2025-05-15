using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempTake_Server.Models
{
    [Table("group")]
    public class Group
    {
        // Primary Key
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        // Data
        [Column("name", TypeName = "varchar(32)")]
        [Required]
        public required string Name { get; set; }
        
        // Timestamps
        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; } = null;
    }
}