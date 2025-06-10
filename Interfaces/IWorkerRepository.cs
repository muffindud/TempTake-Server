using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempTake_Server.Models;

namespace TempTake_Server.Interfaces
{
    public interface IWorkerRepository
    {
        public Task<Worker?> GetWorkerByMacAsync(string mac);
        public Task<int?> GetWorkerIdByMacAsync(string workerMac);
        public Task<Worker?> GetWorkerByIdAsync(int id);
        public Task<Manager?> GetWorkerManagerAsync(int workerId);
        public Task<int?> AddOrGetIdWorkerAsync(string workerMac);
    }
}