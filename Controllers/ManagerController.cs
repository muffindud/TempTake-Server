using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetManagerByMac([FromBody] ManagerDto managerDto)
        {
            var manager = await managerRepository.GetManagerByIdAsync(managerDto.Id);
            if (manager == null) return NotFound("Manager not found");

            return Ok(manager);
        }

        [HttpGet("workers")]
        public async Task<IActionResult> GetManagerWorkers([FromBody] ManagerDto managerDto)
        {
            var manager = await managerRepository.GetManagerByIdAsync(managerDto.Id);
            if (manager == null) return NotFound("Manager not found");

            var workers = await managerRepository.GetManagerWorkersAsync(managerDto.Id);
            return Ok(workers);
        }
    }
}