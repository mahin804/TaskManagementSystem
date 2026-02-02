using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Commands.Task_.TaskCmds
{
    public record UpdateTaskStatusCmd : IRequest<string>
    {
        public int Id { get; init; }
        public bool IsCompleted { get; init; }
    }
}
