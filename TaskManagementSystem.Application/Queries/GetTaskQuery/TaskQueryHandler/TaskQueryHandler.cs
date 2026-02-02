using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Queries.GetTaskQuery.TaskQuery;
using TaskManagementSystem.Application.Response;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.Queries.GetTaskQuery.TaskQueryHandler
{
    public class TaskQueryHandler : IRequestHandler<TaskListQuery, IApiResponse>
    {
        private readonly IRepoServices _repoServices;

        public TaskQueryHandler(IRepoServices repoServices)
        {
            _repoServices = repoServices;
        }
        public async Task<IApiResponse> Handle(TaskListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _repoServices.GetTaskByRole(request.Name,request.Role);
                return new FetchApiExeResult<List<TaskItem>>
                {
                    ResultType = ResultType.Success,
                    Message = "Tasks fetched successfully",
                    Result = new ResultData<List<TaskItem>> { Data = res }
                };
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
