using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TempTake_Server.Dtos.Worker;
using TempTake_Server.Interfaces;

namespace TempTake_Server.Controllers
{
    [Route("api/worker")]
    [ApiController]
    [Authorize]
    public class WorkerController(
        IManagerRepository managerRepository,
        IWorkerRepository workerRepository) 
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetWorkerById([FromBody] WorkerDto workerDto)
        {
            var worker = await workerRepository.GetWorkerByIdAsync(workerDto.Id);
            if (worker == null) return NotFound("Worker not found");

            return Ok(worker);
        }
    }
}