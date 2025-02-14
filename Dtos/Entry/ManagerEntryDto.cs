using System.ComponentModel.DataAnnotations;

namespace TempTake_Server.Dtos.Entry
{
    public class ManagerEntryDto
    {
        [Required]
        public required string ManagerMAC { get; set; }
        public DateTime From { get; set; } = DateTime.Now.AddHours(-24);
        public DateTime To { get; set; } = DateTime.Now;
    }
}