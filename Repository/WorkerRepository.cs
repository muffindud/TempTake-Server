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
        
        public async Task<int?> GetWorkerIdByMacAsync(string mac)
        {
            return GetGetNonZeroOrNull(
                await context.Workers
                    .Where(w => w.Mac == mac)
                    .Select(w => w.Id)
                    .SingleOrDefaultAsync()
            );
        }

        public async Task<Worker?> GetWorkerByMacAsync(string mac)
        {
            return await context.Workers
                .Where(w => w.Mac == mac)
                .SingleOrDefaultAsync();
        }

        public async Task<Manager?> GetWorkerManagerAsync(int workerId)
        {
            return await context.ManagerWorkers
                .Where(mw => mw.WorkerId == workerId)
                .Select(mw => mw.Manager)
                .SingleOrDefaultAsync();
        }

        public async Task<int?> AddOrGetIdWorkerAsync(string workerMac)
        {
            var worker = await GetWorkerByMacAsync(workerMac);
            if (worker != null) return worker.Id;

            worker = new Worker { Mac = workerMac };
            context.Workers.Add(worker);
            await context.SaveChangesAsync();
            return worker.Id;
        }

        public async Task<bool> DeleteWorkerAsync(int workerId)
        {
            var managerWorker = await context.ManagerWorkers
                .Where(mw => mw.WorkerId == workerId && mw.DeletedAt != null)
                .SingleOrDefaultAsync();

            if (managerWorker == null) return false;
            
            managerWorker.DeletedAt = DateTime.UtcNow;
            context.ManagerWorkers.Update(managerWorker);
            await context.SaveChangesAsync();
            return true;
        }
    }
}