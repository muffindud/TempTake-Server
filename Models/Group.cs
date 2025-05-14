using System.ComponentModel.DataAnnotations.Schema;

namespace TempTake_Server.Models
{
    public class Group
    {
        // Primary Key
        public int Id { get; set; }
        
        // Data
        [Column(TypeName = "varchar(32)")]
        public string Name { get; set; } = string.Empty;
        
        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = null;
    }
}