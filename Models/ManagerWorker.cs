using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempTake_Server.Models
{
    [Table("manager_worker")]
    public class ManagerWorker
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
        [ForeignKey("manager")]
        [Column("manager_id")]
        [Required]
        public required int ManagerId { get; set; }
        public Manager? Manager { get; set; }
        
        [ForeignKey("worker")]
        [Column("worker_id")]
        [Required]
        public required int WorkerId { get; set; }
        public Worker? Worker { get; set; }
    }
}