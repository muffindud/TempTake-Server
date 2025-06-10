using TempTake_Server.Models;

namespace TempTake_Server.Interfaces
{
    public interface IManagerRepository
    {
        public Task<Manager?> GetManagerByMacAsync(string mac);
        public Task<int?> GetManagerIdByMac(string managerMac);
        public Task<Manager?> GetManagerByIdAsync(int id);
        public Task<List<Worker?>> GetManagerWorkersAsync(int id);
        public Task<bool> IsWorkerInManagerAsync(int managerId, int workerId);
        public Task<int?> AddWorkerToManagerAsync(int managerId, int workerId);
        public Task<bool> DeleteManagerAsync(int managerId);
    }
}