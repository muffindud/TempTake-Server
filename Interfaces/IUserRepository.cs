using TempTake_Server.Models;

namespace TempTake_Server.Interfaces
{
    public interface IUserRepository
    {
        public Task<int?> GetUserIdByTelegramIdAsync(string telegramId);
        public Task<User?> GetUserByIdAsync(int id);
        public Task<IEnumerable<int>> GetGroupIdsForUserId(int userId);
        public Task<IEnumerable<Group>> GetGroupsForUserIdAsync(int userId);
        public Task<int?> CreateUserAsync(string telegramId, string telegramUsername);
    }
}