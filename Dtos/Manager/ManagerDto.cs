using System.ComponentModel.DataAnnotations;

namespace TempTake_Server.Dtos.Manager;

public class ManagerDto
{
    [Required]
    public required int Id { get; set; }
}