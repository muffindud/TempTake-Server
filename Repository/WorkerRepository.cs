using Microsoft.EntityFrameworkCore;
using TempTake_Server.Context;
using TempTake_Server.Interfaces;
using TempTake_Server.Models;

namespace TempTake_Server.Repository
{
    public class WorkerRepository(ApplicationDbContext context) : RepositoryUtils, IWorkerRepository
    {
        public async Task<Worker?> GetWorkerByIdAsync(int id)
        {
            return await context.Workers.FindAsync(id);
        }
        
        public async Task<int?> GetWorkerIdByMac(string mac)
        {
            return GetGetNonZeroOrNull(
                await context.Workers
                    .Where(w => w.MAC == mac)
                    .Select(w => w.Id)
                    .SingleOrDefaultAsync()
            );
        }

        public async Task<Worker?> GetWorkerByMacAsync(string mac)
        {
            return await context.Workers
                .Where(w => w.MAC == mac)
                .SingleOrDefaultAsync();
        }

        public async Task<Manager?> GetWorkerManagerAsync(int workerId)
        {
            return await context.ManagerWorkers
                .Where(mw => mw.WorkerId == workerId)
                .Select(mw => mw.Manager)
                .SingleOrDefaultAsync();
        }
    }
}