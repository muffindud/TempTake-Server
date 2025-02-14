using System.ComponentModel.DataAnnotations;

namespace TempTake_Server.Dtos.Entry
{
    public class WorkerEntryDto
    {
        [Required]
        public required string WorkerMAC { get; set; }
        public DateTime From { get; set; } = DateTime.Now.AddHours(-24);
        public DateTime To { get; set; } = DateTime.Now;
    }
}