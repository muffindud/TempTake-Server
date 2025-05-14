using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempTake_Server.Models;

namespace TempTake_Server.Interfaces
{
    public interface IManagerRepository
    {
        public Task<Manager?> GetManagerByMacAsync(string mac);
        public Task<int?> GetManagerIdByMac(string managerMac);
        public Task<Manager?> GetManagerByIdAsync(int id);
        public Task<IEnumerable<Worker>> GetManagerWorkersAsync(int id);
    }
}