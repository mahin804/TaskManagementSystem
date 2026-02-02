using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Application.Commands.Task_.TaskCmds;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ISender _sender;
        public TaskController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CreateOrUpdate")]
        [Authorize]
        public async Task<IActionResult> CreateOrUpdateTask([FromBody] CreateTaskCmd registerCmd)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var res = await _sender.Send(registerCmd);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UpdateTaskStatus")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] UpdateTaskStatusCmd Status)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var res = await _sender.Send(Status);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
