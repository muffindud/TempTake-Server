using TempTake_Server.Models;

namespace TempTake_Server.Interfaces
{
    public interface IEntryRepository
    {
        Task<IEnumerable<Entry>> GetWorkerEntriesAsync(int workerId, int count, int page, DateTime from, DateTime to);
        Task<IEnumerable<Entry>> GetAllWorkerEntriesAsync(int workerId, int count, int page);
        Task<Entry?> GetLastWorkerEntryAsync(int workerId);

        Task<IEnumerable<Entry>> GetManagerEntriesAsync(int managerId, int count, int page, DateTime from, DateTime to);
        Task<IEnumerable<Entry>> GetAllManagerEntriesAsync(int managerId, int count, int page);
        Task<Entry?> GetLastManagerEntryAsync(int managerId);
    }
}