using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Response;

namespace TaskManagementSystem.Application.Commands.User.UserCmd
{
    public class UserLoginCmd() : IRequest<IApiResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
