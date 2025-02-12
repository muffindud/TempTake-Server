using TempTake_Server.Models;

namespace TempTake_Server.Interfaces
{
    public interface IEntryRepository
    {
        Task<IEnumerable<Entry>> GetWorkerEntriesAsync(int workerId, DateTime from, DateTime to);
        Task<IEnumerable<Entry>> GetAllWorkerEntriesAsync(int workerId);
        Task<Entry?> GetLastWorkerEntryAsync(int workerId);

        Task<IEnumerable<Entry>> GetManagerEntriesAsync(int managerId, DateTime from, DateTime to);
        Task<IEnumerable<Entry>> GetAllManagerEntriesAsync(int managerId);
        Task<Entry?> GetLastManagerEntryAsync(int managerId);
    }
}