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
    public class ManagerRepository : IManagerRepository
    {
        private readonly ApplicationDbContext _context;

        public ManagerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Manager?> GetManagerByIdAsync(int id)
        {
            return await _context.Managers.FindAsync(id);
        }

        public async Task<Manager?> GetManagerByMACAsync(string mac)
        {
            return await _context.Managers
                .Where(m => m.MAC == mac)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Worker>> GetManagerWorkersAsync(int managerId)
        {
            return await _context.ManagerWorkers
                .Where(mw => mw.ManagerId == managerId)
                .Select(mw => mw.Worker)
                .ToListAsync();
        }
    }
}