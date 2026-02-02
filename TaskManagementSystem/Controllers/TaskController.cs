using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Application.Commands.Task_.TaskCmds;
using TaskManagementSystem.Application.Queries.GetTaskQuery.TaskQuery;
using TaskManagementSystem.Application.Response;
using TaskManagementSystem.Domain.Models;

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
        public async Task<IApiResponse> CreateOrUpdateTask([FromBody] CreateTaskCmd registerCmd)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                registerCmd.OwnerUserId = user;
                var res = await _sender.Send(registerCmd);
                return res;
            }
            catch (Exception ex)
            {
                return new FetchApiExeResult<TaskItem>
                {
                    ResultType = ResultType.Error,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Result = null
                };
            }
        }
        [HttpPost("UpdateTaskStatus")]
        [Authorize(Roles = "Admin")]
        public async Task<IApiResponse> UpdateTaskStatus([FromBody] UpdateTaskStatusCmd Status)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var res = await _sender.Send(Status);
                return res;
            }
            catch(Exception ex)
            {
                return new FetchApiExeResult<TaskItem>
                {
                    ResultType = ResultType.Error,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Result = null
                };
            }
        }
        [HttpGet("GetTaskList")]
        [Authorize]
        public async Task<IApiResponse> GetTaskList([FromQuery] TaskListQuery taskList)
        {
            try
            {
                taskList.Name = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                taskList.Role = User.FindFirst(ClaimTypes.Role)?.Value;
                var res = await _sender.Send(taskList);
                return res;
            }
            catch(Exception ex)
            {
                return new FetchApiExeResult<TaskItem>
                {
                    ResultType = ResultType.Error,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Result = null
                };
            }
        }
    }
}
