using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Commands.Task_.TaskCmds;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.Commands.Task_.TaskCmdHandlers
{
    public class TaskRegisterCmdHandler : IRequestHandler<CreateTaskCmd, string>
    {
        private readonly IRepoServices _repoServices;
        public TaskRegisterCmdHandler(IRepoServices repoServices)
        {
            _repoServices = repoServices;
        }
        public async Task<string> Handle(CreateTaskCmd request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id == 0)
                {
                    var task = new TaskItem
                    {
                        Title = request.Title,
                        Description = request.Description,
                        DueDate = request.DueDate,
                        IsCompleted = false,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _repoServices.AddTask(task);
                    return "Task created successfully";
                }
                else
                {
                    var task = await _repoServices.GetTaskById(request.Id);

                    if (task == null)
                        throw new Exception("Task not found");

                    task.Title = request.Title;
                    task.Description = request.Description;
                    task.DueDate = request.DueDate;

                    await _repoServices.UpdateTask(task);
                    return "Task updated successfully";
                }
                //var Task = new Domain.Models.TaskItem()
                //{
                //    Description = request.Description,
                //    DueDate = request.DueDate,
                //    Title = request.Title,
                //};

                //var res = await _repoServices.AddTask(Task);
                //return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
