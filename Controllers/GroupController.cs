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
        [HttpGet]
        public async Task<IActionResult> GetGroupById([FromBody] GroupDto groupDto)
        {
            var group = await groupRepository.GetGroupByIdAsync(groupDto.Id);
            if (group == null) return NotFound("Group not found");
            
            return Ok(group);
        }
        
        [HttpPost("manager")]
        public async Task<IActionResult> AddManagerToGroup([FromBody] ModuleAddDto moduleAddDto)
        {
            var managerId = await managerRepository.GetManagerIdByMac(moduleAddDto.Mac);
            if (managerId == null) return NotFound("Manager not found, connect it to internet");
            
            if (await groupRepository.IsManagerInGroupAsync((int)managerId, moduleAddDto.Id))
                return BadRequest("Manager already in group");
            
            var groupUserId = await groupRepository.AddManagerToGroupAsync(moduleAddDto.Id, (int)managerId);
            if (groupUserId == null) return BadRequest("Failed to add manager to group");
            
            return Ok(groupUserId);
        }
        
        [HttpGet("managers")]
        public async Task<IActionResult> GetGroupManagers([FromBody] GroupDto groupDto)
        {
            var group = await groupRepository.GetGroupByIdAsync(groupDto.Id);
            if (group == null) return NotFound("Group not found");
            
            var managers = await groupRepository.GetGroupManagersAsync(groupDto.Id);
            return Ok(managers);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsersInGroup([FromBody] GroupDto groupDto)
        {
            var group = await groupRepository.GetGroupByIdAsync(groupDto.Id);
            if (group == null) return NotFound("Group not found");
            
            var users = await groupRepository.GetUsersInGroupAsync(groupDto.Id);
            return Ok(users);
        }

        [HttpGet("workers")]
        public async Task<IActionResult> GetWorkersInGroup([FromBody] GroupDto groupDto)
        {
            var group = await groupRepository.GetGroupByIdAsync(groupDto.Id);
            if (group == null) return NotFound("Group not found");

            var workers = await groupRepository.GetWorkersInGroupAsync(groupDto.Id);
            return Ok(workers);
        }
    }
}