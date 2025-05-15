using Humanizer;
using Microsoft.AspNetCore.Mvc;
using TempTake_Server.Dtos.Entry;
using TempTake_Server.Interfaces;

namespace TempTake_Server.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(
        IUserRepository userRepository, 
        IGroupRepository groupRepository)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserByTelegramId([FromBody] UserDto userDto)
        {
            var userId = await userRepository.GetUserIdByTelegramIdAsync(userDto.TelegramId);
            if (userId == null) return NotFound("User not found");
            
            var user = await userRepository.GetUserByIdAsync((int)userId);
            
            return Ok(user);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var userId = await userRepository.CreateUserAsync(userDto.TelegramId, userDto.TelegramUsername);
            if (userId == null) return BadRequest("Failed to create user");
            
            var groupId = await groupRepository.CreateGroupAsync($"{userDto.TelegramUsername.Truncate(24)}'s group");
            if (groupId == null) return BadRequest("Failed to create group");
            
            var groupUserId = await groupRepository.AddUserToGroupAsync((int)userId, (int)groupId);
            if (groupUserId == null) return BadRequest("Failed to add user to group");
            
            groupUserId = await groupRepository.ConfirmUserGroupJoinAsync((int)userId, (int)groupId);
            if (groupUserId == null) return BadRequest("Failed to confirm user group join");
            
            var groupAdminId = await groupRepository.SetGroupAdminAsync((int)userId, (int)groupId);
            if (groupAdminId == null) return BadRequest("Failed to set group admin");
            return Ok(userId);
        }
        
        [HttpGet("group")]
        public async Task<IActionResult> GetUserGroupsAsync([FromBody] UserDto userDto)
        {
            var userId = await userRepository.GetUserIdByTelegramIdAsync(userDto.TelegramId);
            if (userId == null) return NotFound("User not found");

            var groups = await userRepository.GetGroupsForUserIdAsync((int)userId);
            return Ok(groups);
        }
    }
}