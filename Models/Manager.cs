using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempTake_Server.Models
{
    [Table("manager")]
    public class Manager
    {
        // Primary Key
        [Key]
        [Column("id")]
        public int Id { get; set; }

        // Data
        [Column("mac", TypeName = "varchar(12)")]
        [Required]
        public required string Mac { get; set; }

        // Timestamps
        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}