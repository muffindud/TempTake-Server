using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempTake_Server.Models
{
    [Table("entry")]
    public class Entry
    {
        // Primary Key
        [Key]
        [Column("id")]
        public int Id { get; set; }

        // Data
        [Column("temperature_c", TypeName = "decimal(18, 2)")]
        public decimal? Temperature { get; set; }

        [Column("humidity_perc", TypeName = "decimal(18, 2)")]
        public decimal? Humidity { get; set; }

        [Column("pressure_mmhg", TypeName = "decimal(18, 2)")]
        public decimal? Pressure { get; set; }

        [Column("ppm", TypeName = "decimal(18, 2)")]
        public decimal? Ppm { get; set; }

        // Timestamps
        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        [ForeignKey("manager_worker")]
        [Column("manager_worker_id")]
        [Required]
        public required int ManagerWorkerId { get; set; }
        public ManagerWorker? ManagerWorker { get; set; }
    }
}