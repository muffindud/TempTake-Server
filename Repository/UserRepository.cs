using Humanizer;
using Microsoft.EntityFrameworkCore;
using TempTake_Server.Context;
using TempTake_Server.Interfaces;
using TempTake_Server.Models;

namespace TempTake_Server.Repository
{
    public class UserRepository(ApplicationDbContext context) : RepositoryUtils, IUserRepository
    {
        public async Task<int?> GetUserIdByTelegramIdAsync(string telegramId)
        {
            return GetGetNonZeroOrNull(
                await context.Users
                    .Where(u => u.TelegramUserId == telegramId)
                    .Select(u => u.Id)
                    .SingleOrDefaultAsync()
            );
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<int>> GetGroupIdsForUserId(int userId)
        {
            return await context.GroupUsers
                .Where(gu => gu.UserId == userId)
                .Select(gu => gu.GroupId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetGroupsForUserIdAsync(int userId)
        {
            return await context.GroupUsers
                .Where(gu => gu.UserId == userId)
                .Select(gu => gu.Group)
                .ToListAsync();
        }

        public async Task<int?> CreateUserAsync(string telegramId, string telegramUsername)
        {
            try
            {
                var user = new User
                {
                    TelegramUserId = telegramId,
                    TelegramUsername = telegramUsername
                };
                
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            
                return user.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }
    }
}