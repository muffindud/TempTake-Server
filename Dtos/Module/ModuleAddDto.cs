using System.ComponentModel.DataAnnotations;

namespace TempTake_Server.Dtos.Group
{
    public class ModuleAddDto
    {
        [Required]
        public required int Id { get; set; }
        [Required]
        public required string Mac { get; set; }
    }
}