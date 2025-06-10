using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TempTake_Server.Dtos.Group;
using TempTake_Server.Dtos.Manager;
using TempTake_Server.Interfaces;

namespace TempTake_Server.Controllers
{
    [Route("api/manager")]
    [ApiController]
    [Authorize]
    public class ManagerController(
        IManagerRepository managerRepository,
        IWorkerRepository workerRepository)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetManagerByMac([FromBody] ModuleDto moduleDto)
        {
            var manager = await managerRepository.GetManagerByIdAsync(moduleDto.Id);
            if (manager == null) return NotFound("Manager not found");

            return Ok(manager);
        }

        [HttpGet("workers")]
        public async Task<IActionResult> GetManagerWorkers([FromBody] ModuleDto moduleDto)
        {
            var manager = await managerRepository.GetManagerByIdAsync(moduleDto.Id);
            if (manager == null) return NotFound("Manager not found");

            var workers = await managerRepository.GetManagerWorkersAsync(moduleDto.Id);
            return Ok(workers);
        }

        [HttpPost("worker")]
        public async Task<IActionResult> AddWorkerToManager([FromBody] ModuleAddDto managerWorkerDto)
        {
            var workerId = await workerRepository.GetWorkerIdByMacAsync(managerWorkerDto.Mac);
            if (workerId == null) return NotFound("Worker not found, connect it to internet");

            if (await managerRepository.IsWorkerInManagerAsync((int)workerId, managerWorkerDto.Id))
                return BadRequest("Worker already in manager");

            var managerUserId = await managerRepository.AddWorkerToManagerAsync(managerWorkerDto.Id, (int)workerId);
            if (managerUserId == null) return BadRequest("Failed to add worker to manager");

            return Ok(managerUserId);
        }
    }
}