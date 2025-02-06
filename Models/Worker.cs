using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempTake_Server.Models
{
    public class Worker
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(12)")]
        public required string MAC { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ManagerId { get; set; }
    }
}