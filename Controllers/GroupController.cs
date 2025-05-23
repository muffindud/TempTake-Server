using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TempTake_Server.Dtos.Group;
using TempTake_Server.Interfaces;

namespace TempTake_Server.Controllers
{
    [Route("api/group")]
    [Authorize]
    [ApiController]
    public class GroupController(
        IGroupRepository groupRepository, 
        IManagerRepository managerRepository)
        : ControllerBase
    {
        [HttpPost("manager/add")]
        public async Task<IActionResult> AddManagerToGroup([FromBody] GroupManagerDto groupManagerDto)
        {
            var managerId = await managerRepository.GetManagerIdByMac(groupManagerDto.ManagerMac);
            if (managerId == null) return NotFound("Manager not found, connect it to internet");
            
            if (await groupRepository.IsManagerInGroupAsync((int)managerId, groupManagerDto.GroupId))
                return BadRequest("Manager already in group");
            
            var groupUserId = await groupRepository.AddManagerToGroupAsync((int)managerId, groupManagerDto.GroupId);
            if (groupUserId == null) return BadRequest("Failed to add manager to group");
            
            return Ok(groupUserId);
        }
    }
}