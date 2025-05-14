using System.ComponentModel.DataAnnotations.Schema;

namespace TempTake_Server.Models
{
    public class Manager
    {
        // Primary Key
        public int Id { get; set; }

        // Data
        [Column(TypeName = "varchar(12)")]
        public required string MAC { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}