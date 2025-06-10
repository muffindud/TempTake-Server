using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TempTake_Server.Dtos.Manager;
using TempTake_Server.Interfaces;

namespace TempTake_Server.Controllers
{
    [Route("api/worker")]
    [ApiController]
    [Authorize]
    public class WorkerController(
        IWorkerRepository workerRepository) 
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetWorkerById([FromBody] ModuleDto workerDto)
        {
            var worker = await workerRepository.GetWorkerByIdAsync(workerDto.Id);
            if (worker == null) return NotFound("Worker not found");

            return Ok(worker);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteWorker([FromBody] ModuleDto workerDto)
        {
            var worker = await workerRepository.GetWorkerByIdAsync(workerDto.Id);
            if (worker == null) return NotFound("Worker not found");

            var result = await workerRepository.DeleteWorkerAsync(workerDto.Id);
            if (!result) return BadRequest("Failed to delete worker");

            return Ok("Worker deleted successfully");
        }
    }
}