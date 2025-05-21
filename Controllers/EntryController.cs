using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TempTake_Server.Dtos.Entry;
using TempTake_Server.Interfaces;

namespace TempTake_Server.Controllers
{
    [Route("api/entry")]
    [ApiController]
    [Authorize]
    public class EntryController(
        IEntryRepository entryRepository,
        IManagerRepository managerRepository,
        IWorkerRepository workerRepository)
        : ControllerBase
    {
        [HttpGet("worker")]
        public async Task<IActionResult> GetWorkerEntries([FromBody] WorkerEntryDto entryDto)
        {
            var workerId = await workerRepository.GetWorkerIdByMacAsync(entryDto.WorkerMac);
            if (workerId == null) return NotFound("Worker not found");
            
            var entries = await entryRepository.GetWorkerEntriesAsync((int)workerId, entryDto.From, entryDto.To);
            return Ok(entries);
        }

        [HttpGet("worker/all")]
        public async Task<IActionResult> GetAllWorkerEntries([FromBody] WorkerEntryDto entryDto)
        {
            var workerId = await workerRepository.GetWorkerIdByMacAsync(entryDto.WorkerMac);
            if (workerId == null) return NotFound("Worker not found");
            
            var entries = await entryRepository.GetAllWorkerEntriesAsync((int)workerId);
            return Ok(entries);
        }

        [HttpGet("manager")]
        public async Task<IActionResult> GetManagerEntries([FromBody] ManagerEntryDto entryDto)
        {
            var managerId = await managerRepository.GetManagerIdByMac(entryDto.ManagerMac);
            if (managerId == null) return NotFound("Manager not found");
            
            var entries = await entryRepository.GetManagerEntriesAsync((int)managerId, entryDto.From, entryDto.To);
            return Ok(entries);
        }

        [HttpGet("manager/all")]
        public async Task<IActionResult> GetAllManagerEntries([FromBody] ManagerEntryDto entryDto)
        {
            var managerId = await managerRepository.GetManagerIdByMac(entryDto.ManagerMac);
            if (managerId == null) return NotFound("Manager not found");
            
            var entries = await entryRepository.GetAllManagerEntriesAsync((int)managerId);
            return Ok(entries);
        }
    }
}