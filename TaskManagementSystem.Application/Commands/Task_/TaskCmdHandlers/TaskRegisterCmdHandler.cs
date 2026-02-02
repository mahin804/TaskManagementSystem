using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Commands.Task_.TaskCmds;
using TaskManagementSystem.Application.Response;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.Commands.Task_.TaskCmdHandlers
{
    public class TaskRegisterCmdHandler : IRequestHandler<CreateTaskCmd, IApiResponse>
    {
        private readonly IRepoServices _repoServices;

        public TaskRegisterCmdHandler(IRepoServices repoServices)
        {
            _repoServices = repoServices;
        }

        public async Task<IApiResponse> Handle(CreateTaskCmd request, CancellationToken cancellationToken)
        {
            try
            {
                TaskItem task;

                if (request.Id == 0)
                {
                    task = new TaskItem
                    {
                        Title = request.Title,
                        Description = request.Description,
                        DueDate = request.DueDate,
                        IsCompleted = false,
                        CreatedAt = DateTime.UtcNow,
                        OwnerUserId = request.OwnerUserId
                    };

                    await _repoServices.AddTask(task);

                    return new FetchApiExeResult<TaskItem>
                    {
                        ResultType = ResultType.Success,
                        Message = "Task created successfully",
                        Result = new ResultData<TaskItem> { Data = task }
                    };
                }
                else
                {
                    task = await _repoServices.GetTaskById(request.Id);

                    if (task == null)
                    {
                        return new FetchApiExeResult<TaskItem>
                        {
                            ResultType = ResultType.NotFound,
                            Message = "Task not found",
                            Result = null
                        };
                    }

                    task.Title = request.Title;
                    task.Description = request.Description;
                    task.DueDate = request.DueDate;

                    await _repoServices.UpdateTask(task);

                    return new FetchApiExeResult<TaskItem>
                    {
                        ResultType = ResultType.Success,
                        Message = "Task updated successfully",
                        Result = new ResultData<TaskItem> { Data = task }
                    };
                }
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
    }
}
