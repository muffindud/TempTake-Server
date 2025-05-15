using Microsoft.EntityFrameworkCore;
using TempTake_Server.Context;
using TempTake_Server.Interfaces;
using TempTake_Server.Models;

namespace TempTake_Server.Repository
{
    public class GroupRepository(ApplicationDbContext context) : RepositoryUtils, IGroupRepository
    {
        public async Task<Group?> GetGroupByIdAsync(int id)
        {

            return await context.Groups
                .Where(g => g.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<int?> CreateGroupAsync(string name)
        {
            try
            {
                var group = new Group
                {
                    Name = name
                };
                
                await context.Groups.AddAsync(group);
                await context.SaveChangesAsync();
                
                return group.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<int?> SetGroupAdminAsync(int userId, int groupId)
        {
            try
            {
                var groupUser = await context.GroupUsers
                    .Where(gu => gu.UserId == userId && gu.GroupId == groupId && gu.IsConfirmed && gu.DeletedAt == null)
                    .SingleOrDefaultAsync();

                if (groupUser == null) return null;
                
                groupUser.IsAdmin = true;
                await context.SaveChangesAsync();
                
                return groupUser.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<int?> AddUserToGroupAsync(int userId, int groupId)
        {
            try
            {
                var groupUser = new GroupUser
                {
                    UserId = userId,
                    GroupId = groupId
                };
                
                await context.GroupUsers.AddAsync(groupUser);
                await context.SaveChangesAsync();
                
                return groupUser.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<int?> ConfirmUserGroupJoinAsync(int userId, int groupId)
        {
            try
            {
                var groupUser = await context.GroupUsers
                    .Where(gu => gu.UserId == userId && gu.GroupId == groupId && gu.DeletedAt == null)
                    .SingleOrDefaultAsync();

                if (groupUser == null) return null;
                
                groupUser.IsConfirmed = true;
                await context.SaveChangesAsync();

                return groupUser.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<int?> RemoveUserFromGroupAsync(int userId, int groupId)
        {
            try
            {
                var groupUser = await context.GroupUsers
                    .Where(gu => gu.UserId == userId && gu.GroupId == groupId && gu.DeletedAt == null)
                    .SingleOrDefaultAsync();

                if (groupUser == null) return null;
                
                groupUser.DeletedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                
                return groupUser.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<User?>> GetUsersInGroupAsync(int groupId)
        {
            return await context.GroupUsers
                .Where(gu => gu.GroupId == groupId && gu.IsConfirmed && gu.DeletedAt == null)
                .Select(gu => gu.User)
                .ToListAsync();
        }
    }
}