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
        public async Task<IActionResult> GetWorkerEntries([FromBody] ModuleEntryDto entryDto)
        {
            var workerId = entryDto.Id;
            
            if (workerId == null)
            {
                if (string.IsNullOrEmpty(entryDto.ModuleMac))
                {
                    return BadRequest("Module MAC address or id is required");
                }

                workerId = await workerRepository.GetWorkerIdByMacAsync(entryDto.ModuleMac);
                if (workerId == null) return NotFound("Worker not found");
            }
            
            var entries = await entryRepository.GetWorkerEntriesAsync(
                (int)workerId,
                entryDto.Count,
                entryDto.Page,
                entryDto.From, 
                entryDto.To
            );
            return Ok(entries);
        }

        [HttpGet("worker/all")]
        public async Task<IActionResult> GetAllWorkerEntries([FromBody] ModuleEntryDto entryDto)
        {
            var workerId = entryDto.Id;
            
            if (workerId == null)
            {
                if (string.IsNullOrEmpty(entryDto.ModuleMac))
                {
                    return BadRequest("Module MAC address or id is required");
                }

                workerId = await managerRepository.GetManagerIdByMac(entryDto.ModuleMac);
                if (workerId == null) return NotFound("Manager not found");
            }
            
            var entries = await entryRepository.GetAllWorkerEntriesAsync(
                (int)workerId,
                entryDto.Count,
                entryDto.Page
            );
            return Ok(entries);
        }

        [HttpGet("manager")]
        public async Task<IActionResult> GetManagerEntries([FromBody] ModuleEntryDto entryDto)
        {
            var managerId = entryDto.Id;
            
            if (managerId == null)
            {
                if (string.IsNullOrEmpty(entryDto.ModuleMac))
                {
                    return BadRequest("Manager MAC address or id is required");
                }

                managerId = await managerRepository.GetManagerIdByMac(entryDto.ModuleMac);
                if (managerId == null) return NotFound("Manager not found");
            }
            
            var entries = await entryRepository.GetManagerEntriesAsync(
                (int)managerId,
                entryDto.Count,
                entryDto.Page,
                entryDto.From,
                entryDto.To
            );
            return Ok(entries);
        }

        [HttpGet("manager/all")]
        public async Task<IActionResult> GetAllManagerEntries([FromBody] ModuleEntryDto entryDto)
        {
            var managerId = entryDto.Id;
            
            if (managerId == null)
            {
                if (string.IsNullOrEmpty(entryDto.ModuleMac))
                {
                    return BadRequest("Manager MAC address or id is required");
                }

                managerId = await managerRepository.GetManagerIdByMac(entryDto.ModuleMac);
                if (managerId == null) return NotFound("Manager not found");
            }
            
            var entries = await entryRepository.GetAllManagerEntriesAsync(
                (int)managerId,
                entryDto.Count,
                entryDto.Page
            );
            return Ok(entries);
        }
    }
}