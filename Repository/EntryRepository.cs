using Microsoft.EntityFrameworkCore;
using TempTake_Server.Context;
using TempTake_Server.Interfaces;
using TempTake_Server.Models;

namespace TempTake_Server.Repository
{
    public class EntryRepository(ApplicationDbContext context) : IEntryRepository
    {
        private async Task<int?> GetWorkerIdByMac(string workerMac)
        {
            return await context.Workers
                .Where(w => w.MAC == workerMac)
                .Select(w => w.Id)
                .FirstOrDefaultAsync();
        }

        private async Task<int?> GetManagerIdByMac(string managerMac)
        {
            return await context.Managers
                .Where(m => m.MAC == managerMac)
                .Select(m => m.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Entry>> GetAllManagerEntriesAsync(string managerMac)
        {
            var managerId = await this.GetManagerIdByMac(managerMac);

            if (managerId == null)
                return new List<Entry>();

            var entries = await (context.Entries
                .Join(
                    context.ManagerWorkers,
                    e => e.WorkerId,
                    mw => mw.WorkerId,
                    (e, mw) => new { e, mw }
                ).Where(@t =>
                    @t.mw.ManagerId == @managerId &&
                    @t.mw.DeletedAt == null &&
                    @t.e.CreatedAt >= @t.mw.CreatedAt
                ).Select(@t => @t.e)).ToListAsync();

            return entries;
        }

        public async Task<IEnumerable<Entry>> GetAllWorkerEntriesAsync(string workerMac)
        {
            var workerId = await this.GetWorkerIdByMac(workerMac);

            if (workerId == null)
                return new List<Entry>();

            var entries = await context.Entries
                .Where(e => e.WorkerId == workerId)
                .ToListAsync();

            return entries;
        }

        public async Task<Entry?> GetLastManagerEntryAsync(string managerMac)
        {
            var managerId = await this.GetManagerIdByMac(managerMac);

            if (managerId == null)
                return null;

            var entry = await context.Entries
                .Join(
                    context.ManagerWorkers,
                    e => e.WorkerId,
                    mw => mw.WorkerId,
                    (e, mw) => new { e, mw }
                ).Where(@t =>
                    @t.mw.ManagerId == @managerId &&
                    @t.mw.DeletedAt == null
                ).OrderByDescending(@t => @t.e.CreatedAt)
                .Select(@t => @t.e)
                .FirstOrDefaultAsync();

            return entry;
        }

        public async Task<Entry?> GetLastWorkerEntryAsync(string workerMac)
        {
            var workerId = await this.GetWorkerIdByMac(workerMac);

            if (workerId == null)
                return null;

            var entry = await context.Entries
                .Where(e => e.WorkerId == workerId)
                .OrderByDescending(e => e.CreatedAt)
                .FirstOrDefaultAsync();

            return entry;
        }

        public async Task<IEnumerable<Entry>> GetManagerEntriesAsync(string managerMac, DateTime from, DateTime to)
        {
            var managerId = await this.GetManagerIdByMac(managerMac);

            if (managerId == null)
                return new List<Entry>();

            var entries = await context.Entries
                .Join(
                    context.ManagerWorkers,
                    e => e.WorkerId,
                    mw => mw.WorkerId,
                    (e, mw) => new { e, mw }
                ).Where(@t =>
                    @t.mw.ManagerId == @managerId &&
                    @t.mw.DeletedAt == null &&
                    @t.e.CreatedAt >= @t.mw.CreatedAt &&
                    @t.e.CreatedAt >= @from &&
                    @t.e.CreatedAt <= @to
                ).Select(@t => @t.e)
                .ToListAsync();

            return entries;
        }

        public async Task<IEnumerable<Entry>> GetWorkerEntriesAsync(string workerMac, DateTime from, DateTime to)
        {
            var workerId = await this.GetWorkerIdByMac(workerMac);

            if (workerId == null)
                return new List<Entry>();

            DateTime? registeredAt = await context.ManagerWorkers
                .Where(mw => mw.WorkerId == workerId && mw.DeletedAt == null)
                .Select(mw => mw.CreatedAt)
                .FirstOrDefaultAsync();

            if (registeredAt == null)
                return new List<Entry>();

            var entries = await context.Entries
                .Where(e =>
                    e.CreatedAt >= registeredAt &&
                    e.WorkerId == workerId &&
                    e.CreatedAt >= from &&
                    e.CreatedAt <= to
                ).ToListAsync();

            return entries;
        }
    }
}