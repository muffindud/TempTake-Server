using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TempTake_Server.Models
{
    public class Entry
    {
        // Primary Key
        public int Id { get; set; }

        // Data
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Temperature { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Humidity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Pressure { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Ppm { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}