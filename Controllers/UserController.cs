using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TempTake_Server.Dtos.Entry;
using TempTake_Server.Dtos.User;
using TempTake_Server.Interfaces;

namespace TempTake_Server.Controllers
{
    [Route("api/user")]
    [Authorize]
    [ApiController]
    public class UserController(
        IUserRepository userRepository, 
        IGroupRepository groupRepository)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserByTelegramId([FromBody] UserDto userDto)
        {
            var user = await userRepository.GetUserByTelegramIdAsync(userDto.TelegramId);
            if (user == null) return NotFound("User not found");

            return Ok(user);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var user = await userRepository.GetUserByTelegramIdAsync(userDto.TelegramId);
            if (user != null) return Ok(user);
            
            user = await userRepository.CreateUserAsync(userDto.TelegramId, userDto.TelegramUsername);
            if (user == null) return BadRequest("Failed to create user");
            
            var group = await groupRepository.CreateGroupAsync($"{userDto.TelegramUsername.Truncate(24)}'s group");
            if (group == null) return BadRequest("Failed to create group");
            
            var groupUserId = await groupRepository.AddUserToGroupAsync(user.Id, group.Id);
            if (groupUserId == null) return BadRequest("Failed to add user to group");
            
            groupUserId = await groupRepository.ConfirmUserGroupJoinAsync(user.Id, group.Id);
            if (groupUserId == null) return BadRequest("Failed to confirm user group join");
            
            var groupAdminId = await groupRepository.SetGroupAdminAsync(user.Id, group.Id);
            if (groupAdminId == null) return BadRequest("Failed to set group admin");
            
            return Ok(user);
        }
        
        [HttpGet("groups")]
        public async Task<IActionResult> GetUserGroupsAsync([FromBody] UserDto userDto)
        {
            var userId = await userRepository.GetUserIdByTelegramIdAsync(userDto.TelegramId);
            if (userId == null) return NotFound("User not found");

            var groups = await userRepository.GetGroupsForUserIdAsync((int)userId);
            return Ok(groups);
        }
    }
}