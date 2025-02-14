using Microsoft.AspNetCore.Mvc;
using TempTake_Server.Context;
using TempTake_Server.Dtos.Entry;
using TempTake_Server.Interfaces;

namespace TempTake_Server.Controllers
{
    [Route("api/entry")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEntryRepository _entryRepository;

        public EntryController(ApplicationDbContext context, IEntryRepository entryRepository) {
            _context = context;
            _entryRepository = entryRepository;
        }

        [HttpGet("worker")]
        public async Task<IActionResult> GetWorkerEntries([FromBody] WorkerEntryDto entryDto)
        {
            if (entryDto.WorkerMAC == null)
                return BadRequest("WorkerMAC is required");

            var entries = await _entryRepository.GetWorkerEntriesAsync(entryDto.WorkerMAC, entryDto.From, entryDto.To);
            return Ok(entries);
        }

        [HttpGet("worker/all")]
        public async Task<IActionResult> GetAllWorkerEntries([FromBody] WorkerEntryDto entryDto)
        {
            if (entryDto.WorkerMAC == null)
                return BadRequest("WorkerMAC is required");

            var entries = await _entryRepository.GetAllWorkerEntriesAsync(entryDto.WorkerMAC);
            return Ok(entries);
        }

        [HttpGet("manager")]
        public async Task<IActionResult> GetManagerEntries([FromBody] ManagerEntryDto entryDto)
        {
            if (entryDto.ManagerMAC == null)
                return BadRequest("ManagerMAC is required");

            var entries = await _entryRepository.GetManagerEntriesAsync(entryDto.ManagerMAC, entryDto.From, entryDto.To);
            return Ok(entries);
        }

        [HttpGet("manager/all")]
        public async Task<IActionResult> GetAllManagerEntries([FromBody] ManagerEntryDto entryDto)
        {
            if (entryDto.ManagerMAC == null)
                return BadRequest("ManagerMAC is required");

            var entries = await _entryRepository.GetAllManagerEntriesAsync(entryDto.ManagerMAC);
            return Ok(entries);
        }
    }
}