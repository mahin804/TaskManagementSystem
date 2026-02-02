using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Commands.Task_.TaskCmds;
using TaskManagementSystem.Domain.Interfaces;

namespace TaskManagementSystem.Application.Commands.Task_.TaskCmdHandlers
{
    public class UpdateTaskStatusCmdHandler : IRequestHandler<UpdateTaskStatusCmd, string>
    {
        private readonly IRepoServices _repoServices;

        public UpdateTaskStatusCmdHandler(IRepoServices repoServices)
        {
            _repoServices = repoServices;
        }
        public async Task<string> Handle(UpdateTaskStatusCmd request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _repoServices.GetTaskById(request.Id);

                if (task == null)
                    throw new Exception("Task not found");

                task.IsCompleted = request.IsCompleted;

                await _repoServices.UpdateStatus(request.Id, task.IsCompleted);

                return "Task status updated successfully";
            }
            catch(Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
