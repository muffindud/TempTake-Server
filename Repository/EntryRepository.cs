using Microsoft.EntityFrameworkCore;
using TempTake_Server.Context;
using TempTake_Server.Interfaces;
using TempTake_Server.Models;

namespace TempTake_Server.Repository
{
    public class EntryRepository(ApplicationDbContext context) : IEntryRepository
    {
        public async Task<IEnumerable<Entry>> GetAllManagerEntriesAsync(int managerId)
        {
            var entries = await context.Entries
                .Join(
                    context.ManagerWorkers,
                    e => e.WorkerId,
                    mw => mw.WorkerId,
                    (e, mw) => new { e, mw }
                ).Where(@t =>
                    @t.mw.ManagerId == @managerId &&
                    @t.mw.DeletedAt == null &&
                    @t.e.CreatedAt >= @t.mw.CreatedAt
                ).Select(@t => @t.e)
                .ToListAsync();

            return entries;
        }

        public async Task<IEnumerable<Entry>> GetAllWorkerEntriesAsync(int workerId)
        {
            var entries = await context.Entries
                .Where(e => e.WorkerId == workerId)
                .ToListAsync();

            return entries;
        }

        public async Task<Entry?> GetLastManagerEntryAsync(int managerId)
        {
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
                .FirstAsync();

            return entry;
        }

        public async Task<Entry?> GetLastWorkerEntryAsync(int workerId)
        {
            var entry = await context.Entries
                .Where(e => e.WorkerId == workerId)
                .OrderByDescending(e => e.CreatedAt)
                .FirstAsync();

            return entry;
        }

        public async Task<IEnumerable<Entry>> GetManagerEntriesAsync(int managerId, DateTime from, DateTime to)
        {
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

        public async Task<IEnumerable<Entry>> GetWorkerEntriesAsync(int workerId, DateTime from, DateTime to)
        {
            DateTime? registeredAt = await context.ManagerWorkers
                .Where(mw => mw.WorkerId == workerId && mw.DeletedAt == null)
                .Select(mw => mw.CreatedAt)
                .FirstOrDefaultAsync();

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