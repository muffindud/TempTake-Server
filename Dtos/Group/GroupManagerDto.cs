using System.ComponentModel.DataAnnotations;

namespace TempTake_Server.Dtos.Group
{
    public class GroupManagerDto
    {
        [Required]
        public required int GroupId { get; set; }
        [Required]
        public required string ManagerMac { get; set; }
    }
}