using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Entry>> GetAllManagerEntriesAsync(int managerId)
        {
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

        public async Task<IEnumerable<Entry>> GetAllWorkerEntriesAsync(int workerId)
        {
            return await _context.Entries
                .Where(e => e.WorkerId == workerId)
                .ToListAsync();
        }

        public async Task<Entry?> GetLastManagerEntryAsync(int managerId)
        {
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

        public async Task<Entry?> GetLastWorkerEntryAsync(int workerId)
        {
            return await _context.Entries
                .Where(e => e.WorkerId == workerId)
                .OrderByDescending(e => e.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Entry>> GetManagerEntriesAsync(int managerId, DateTime from, DateTime to)
        {
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

        public async Task<IEnumerable<Entry>> GetWorkerEntriesAsync(int workerId, DateTime from, DateTime to)
        {
            return await _context.Entries
                .Where(e =>
                    e.WorkerId == workerId &&
                    e.CreatedAt >= from &&
                    e.CreatedAt <= to
                )
                .ToListAsync();
        }
    }
}