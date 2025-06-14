using Microsoft.EntityFrameworkCore;
using TempTake_Server.Context;
using TempTake_Server.Interfaces;
using TempTake_Server.Models;

namespace TempTake_Server.Repository
{
    public class EntryRepository(ApplicationDbContext context) : IEntryRepository
    {
        public async Task<IEnumerable<Entry>> GetAllManagerEntriesAsync(int managerId, int count, int page)
        {
            var entries = await context.Entries
                .Where(e =>
                    context.ManagerWorkers
                        .Where(mw => mw.ManagerId == managerId && mw.DeletedAt == null)
                        .Select(mw => mw.Id)
                        .Contains(e.ManagerWorkerId)
                ).OrderByDescending(e => e.CreatedAt)
                .Skip((page - 1) * count)
                .Take(count)
                .ToListAsync();
            
            return entries;
        }

        public async Task<IEnumerable<Entry>> GetAllWorkerEntriesAsync(int workerId, int count, int page)
        {
            var entries = await context.Entries
                .Where(e =>
                    context.ManagerWorkers
                        .Where(mw => mw.WorkerId == workerId && mw.DeletedAt == null)
                        .Select(mw => mw.Id)
                        .Contains(e.ManagerWorkerId)
                ).OrderByDescending(e => e.CreatedAt)
                .Skip((page - 1) * count)
                .Take(count)
                .ToListAsync();
            
            return entries;
        }

        public async Task<Entry?> GetLastManagerEntryAsync(int managerId)
        {
            var entry = await context.Entries
                .Where(e =>
                    context.ManagerWorkers
                        .Where(mw => mw.ManagerId == managerId && mw.DeletedAt == null)
                        .Select(mw => mw.Id)
                        .Contains(e.ManagerWorkerId)
                ).OrderByDescending(e => e.CreatedAt)
                .FirstAsync();
            
            return entry;
        }

        public async Task<Entry?> GetLastWorkerEntryAsync(int workerId)
        {
            var entry = await context.Entries
                .Where(e =>
                    context.ManagerWorkers
                        .Where(mw => mw.WorkerId == workerId && mw.DeletedAt == null)
                        .Select(mw => mw.Id)
                        .Contains(e.ManagerWorkerId)
                ).OrderByDescending(e => e.CreatedAt)
                .FirstAsync();
                
            return entry;
        }

        public async Task<IEnumerable<Entry>> GetManagerEntriesAsync(int managerId, int count, int page, DateTime from, DateTime to)
        {
            var entries = await context.Entries
                .Where(e =>
                    context.ManagerWorkers
                        .Where(mw => mw.ManagerId == managerId && mw.DeletedAt == null)
                        .Select(mw => mw.Id)
                        .Contains(e.ManagerWorkerId) &&
                    e.CreatedAt >= from &&
                    e.CreatedAt <= to
                ).OrderByDescending(e => e.CreatedAt)
                .Skip((page - 1) * count)
                .Take(count)
                .ToListAsync();
            
            return entries;
        }

        public async Task<IEnumerable<Entry>> GetWorkerEntriesAsync(int workerId, int count, int page, DateTime from, DateTime to)
        {
            var entries = await context.Entries
                .Where(e =>
                    context.ManagerWorkers
                        .Where(mw => mw.WorkerId == workerId && mw.DeletedAt == null)
                        .Select(mw => mw.Id)
                        .Contains(e.ManagerWorkerId) &&
                    e.CreatedAt >= from &&
                    e.CreatedAt <= to
                ).OrderByDescending(e => e.CreatedAt)
                .Skip((page - 1) * count)
                .Take(count)
                .ToListAsync();
            
            return entries;
        }
    }
}