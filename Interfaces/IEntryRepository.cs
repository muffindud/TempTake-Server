using TempTake_Server.Models;

namespace TempTake_Server.Interfaces
{
    public interface IEntryRepository
    {
        Task<IEnumerable<Entry>> GetWorkerEntriesAsync(string workerMAC, DateTime from, DateTime to);
        Task<IEnumerable<Entry>> GetAllWorkerEntriesAsync(string workerMAC);
        Task<Entry?> GetLastWorkerEntryAsync(string workerMAC);

        Task<IEnumerable<Entry>> GetManagerEntriesAsync(string managerMAC, DateTime from, DateTime to);
        Task<IEnumerable<Entry>> GetAllManagerEntriesAsync(string managerMAC);
        Task<Entry?> GetLastManagerEntryAsync(string managerMAC);
    }
}