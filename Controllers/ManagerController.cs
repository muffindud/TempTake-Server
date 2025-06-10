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
    
        [HttpDelete]
        public async Task<IActionResult> DeleteManager([FromBody] ModuleDto moduleDto)
        {
            var manager = await managerRepository.GetManagerByIdAsync(moduleDto.Id);
            if (manager == null) return NotFound("Manager not found");

            var result = await managerRepository.DeleteManagerAsync(moduleDto.Id);
            if (!result) return BadRequest("Failed to delete manager");

            return Ok("Manager deleted successfully");
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
            var workerId = await workerRepository.AddOrGetIdWorkerAsync(managerWorkerDto.Mac);
            
            if (await managerRepository.IsWorkerInManagerAsync((int)workerId, managerWorkerDto.Id))
                return BadRequest("Worker already in manager");

            var managerUserId = await managerRepository.AddWorkerToManagerAsync(managerWorkerDto.Id, (int)workerId);
            if (managerUserId == null) return BadRequest("Failed to add worker to manager");

            return Ok(managerUserId);
        }
    }
}