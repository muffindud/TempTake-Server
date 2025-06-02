using System.ComponentModel.DataAnnotations;

namespace TempTake_Server.Dtos.Group;

public class GroupDto
{
    [Required]
    public required int Id { get; set; }
}