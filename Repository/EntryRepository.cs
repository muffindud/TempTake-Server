using Microsoft.EntityFrameworkCore;
using TempTake_Server.Context;
using TempTake_Server.Interfaces;
using TempTake_Server.Models;

namespace TempTake_Server.Repository
{
    public class EntryRepository : IEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public EntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entry>> GetAllManagerEntriesAsync(string workerMAC)
        {
            int? managerId = await _context.Managers
                .Where(m => m.MAC == workerMAC)
                .Select(m => m.Id)
                .FirstOrDefaultAsync();

            if (managerId == null)
                return new List<Entry>();

            List<ManagerWorker> managerWorkers = await _context.ManagerWorkers
                .Where(mw =>
                    mw.ManagerId == managerId &&
                    mw.DeletedAt == null
                )
                .ToListAsync();

            return await _context.Entries
                .Where(e => managerWorkers.Any(mw => mw.WorkerId == e.WorkerId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Entry>> GetAllWorkerEntriesAsync(string workerMAC)
        {
            int? workerId = await _context.Workers
                .Where(w => w.MAC == workerMAC)
                .Select(w => w.Id)
                .FirstOrDefaultAsync();

            if (workerId == null)
                return new List<Entry>();

            return await _context.Entries
                .Where(e => e.WorkerId == workerId)
                .ToListAsync();
        }

        public async Task<Entry?> GetLastManagerEntryAsync(string managerMAC)
        {
            int? managerId = await _context.Managers
                .Where(m => m.MAC == managerMAC)
                .Select(m => m.Id)
                .FirstOrDefaultAsync();

            if (managerId == null)
                return null;

            List<ManagerWorker> managerWorkers = await _context.ManagerWorkers
                .Where(mw =>
                    mw.ManagerId == managerId &&
                    mw.DeletedAt == null
                )
                .ToListAsync();

            return await _context.Entries
                .Where(e => managerWorkers.Any(mw => mw.WorkerId == e.WorkerId))
                .OrderByDescending(e => e.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<Entry?> GetLastWorkerEntryAsync(string workerMAC)
        {
            int? workerId = _context.Workers
                .Where(w => w.MAC == workerMAC)
                .Select(w => w.Id)
                .FirstOrDefault();

            if (workerId == null)
                return null;

            return await _context.Entries
                .Where(e => e.WorkerId == workerId)
                .OrderByDescending(e => e.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Entry>> GetManagerEntriesAsync(string managerMAC, DateTime from, DateTime to)
        {
            int? managerId = await _context.Managers
                .Where(m => m.MAC == managerMAC)
                .Select(m => m.Id)
                .FirstOrDefaultAsync();

            if (managerId == null)
                return new List<Entry>();

            List<ManagerWorker> managerWorkers = await _context.ManagerWorkers
                .Where(mw =>
                    mw.ManagerId == managerId &&
                    mw.DeletedAt == null
                )
                .ToListAsync();

            return await _context.Entries
                .Where(e =>
                    managerWorkers.Any(mw => mw.WorkerId == e.WorkerId) &&
                    managerWorkers.Any(mw => e.CreatedAt >= mw.CreatedAt) &&
                    e.CreatedAt >= from &&
                    e.CreatedAt <= to
                )
                .ToListAsync();
        }

        public async Task<IEnumerable<Entry>> GetWorkerEntriesAsync(string workerMAC, DateTime from, DateTime to)
        {
            int? workerId = await _context.Workers
                .Where(w => w.MAC == workerMAC)
                .Select(w => w.Id)
                .FirstOrDefaultAsync();

            if (workerId == null)
                return new List<Entry>();

            DateTime? registeredAt = await _context.ManagerWorkers
                .Where(mw => mw.WorkerId == workerId && mw.DeletedAt == null)
                .Select(mw => mw.CreatedAt)
                .FirstOrDefaultAsync();

            if (registeredAt == null)
                return new List<Entry>();

            return await _context.Entries
                .Where(e =>
                    e.CreatedAt >= registeredAt &&
                    e.WorkerId == workerId &&
                    e.CreatedAt >= from &&
                    e.CreatedAt <= to
                )
                .ToListAsync();
        }
    }
}