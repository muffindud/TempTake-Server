using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempTake_Server.Models;

namespace TempTake_Server.Interfaces
{
    public interface IWorkerRepository
    {
        public Task<Worker?> GetWorkerByMACAsync(string mac);
        public Task<Worker?> GetWorkerByIdAsync(int id);
        public Task<Manager?> GetWorkerManagerAsync(int workerId);
    }
}