using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.Application.Commands.Task_.TaskCmds;
using TaskManagementSystem.Application.Commands.User.UserCmd;
using TaskManagementSystem.Application.Response;
using TaskManagementSystem.Authoriz;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        public UserController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("login")]
        public async Task<IApiResponse> Login([FromBody] UserLoginCmd UserLogin)
        {
            try
            {
                var res = await _sender.Send(UserLogin);
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
