using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TempTake_Server.Models
{
    public class Entry
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Temperature { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Humidity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Pressure { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Ppm { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ManagerWorkerId { get; set; }
    }
}