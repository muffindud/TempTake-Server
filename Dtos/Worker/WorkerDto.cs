using System.ComponentModel.DataAnnotations;

namespace TempTake_Server.Dtos.Worker;

public class WorkerDto
{
    [Required]
    public required int Id { get; set; }
}