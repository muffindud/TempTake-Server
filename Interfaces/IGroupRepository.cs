using TempTake_Server.Models;

namespace TempTake_Server.Interfaces
{
    public interface IGroupRepository
    {
        public Task<Group?> GetGroupByIdAsync(int id);
        public Task<Group?> CreateGroupAsync(string name);
        public Task<int?> SetGroupAdminAsync(int userId, int groupId);
        public Task<int?> AddUserToGroupAsync(int userId, int groupId);
        public Task<int?> ConfirmUserGroupJoinAsync(int userId, int groupId);
        public Task<int?> RemoveUserFromGroupAsync(int userId, int groupId);
        public Task<int?> AddManagerToGroupAsync(int groupId, int managerId);
        public Task<List<User?>> GetUsersInGroupAsync(int groupId);
    }
}