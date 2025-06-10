using Microsoft.EntityFrameworkCore;
using TempTake_Server.Context;
using TempTake_Server.Interfaces;
using TempTake_Server.Models;

namespace TempTake_Server.Repository
{
    public class ManagerRepository(ApplicationDbContext context) : RepositoryUtils, IManagerRepository
    {
        public async Task<Manager?> GetManagerByIdAsync(int id)
        {
            return await context.Managers.FindAsync(id);
        }

        public async Task<int?> GetManagerIdByMac(string mac)
        {
            return GetGetNonZeroOrNull(
                await context.Managers
                    .Where(m => m.Mac == mac)
                    .Select(m => m.Id)
                    .SingleOrDefaultAsync()
            );
        }

        public async Task<Manager?> GetManagerByMacAsync(string mac)
        {
            return await context.Managers
                .Where(m => m.Mac == mac)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Worker?>> GetManagerWorkersAsync(int id)
        {
            return await context.ManagerWorkers
                .Where(mw => mw.ManagerId == id && mw.DeletedAt == null)
                .Select(mw => mw.Worker)
                .ToListAsync();
        }

        public async Task<bool> IsWorkerInManagerAsync(int managerId, int workerId)
        {
            return await context.ManagerWorkers
                .AnyAsync(mw => mw.ManagerId == managerId && mw.WorkerId == workerId);
        }

        public async Task<int?> AddWorkerToManagerAsync(int managerId, int workerId)
        {
            var managerWorker = new ManagerWorker
            {
                ManagerId = managerId,
                WorkerId = workerId
            };
            
            await context.ManagerWorkers.AddAsync(managerWorker);
            await context.SaveChangesAsync();
            
            return managerWorker.Id;
        }

        public async Task<bool> DeleteManagerAsync(int managerId)
        {
            var groupManager = await context.GroupManagers
                .Where(gm => gm.ManagerId == managerId && gm.DeletedAt == null)
                .SingleOrDefaultAsync();

            if (groupManager == null) return false;

            groupManager.DeletedAt = DateTime.UtcNow;
            context.GroupManagers.Update(groupManager);
            await context.SaveChangesAsync();
            return true;
        }
    }
}