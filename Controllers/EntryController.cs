using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TempTake_Server.Context;
using TempTake_Server.Dtos;
using TempTake_Server.Dtos.Entry;
using TempTake_Server.Interfaces;
using TempTake_Server.Models;

namespace TempTake_Server.Controllers
{
    [Route("api/entry")]
    [ApiController]
    class EntryController : ControllerBase {
        private readonly ApplicationDbContext _context;
        private readonly IEntryRepository _entryRepository;

        public EntryController(ApplicationDbContext context, IEntryRepository entryRepository) {
            _context = context;
            _entryRepository = entryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkerEntries([FromBody] EntryDto entryDto)
        {
            var entries = await _entryRepository.GetWorkerEntriesAsync(entryDto.WorkerMAC, entryDto.From, entryDto.To);
            return Ok(entries);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWorkerEntries([FromBody] EntryDto entryDto)
        {
            var entries = await _entryRepository.GetAllWorkerEntriesAsync(entryDto.WorkerMAC);
            return Ok(entries);
        }
    }
}