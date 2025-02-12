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
    public class WorkerRepository : IWorkerRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Worker?> GetWorkerByIdAsync(int id)
        {
            return await _context.Workers.FindAsync(id);
        }

        public async Task<Worker?> GetWorkerByMACAsync(string mac)
        {
            return await _context.Workers
                .Where(w => w.MAC == mac)
                .FirstOrDefaultAsync();
        }

        public async Task<Manager?> GetWorkerManagerAsync(int workerId)
        {
            return await _context.ManagerWorkers
                .Where(mw => mw.WorkerId == workerId)
                .Select(mw => mw.Manager)
                .FirstOrDefaultAsync();
        }
    }
}