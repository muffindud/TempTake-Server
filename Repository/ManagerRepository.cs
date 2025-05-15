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
                .Where(mw => mw.ManagerId == id)
                .Select(mw => mw.Worker)
                .ToListAsync();
        }
    }
}