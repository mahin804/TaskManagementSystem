using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Response;

namespace TaskManagementSystem.Application.Queries.GetTaskQuery.TaskQuery
{
    public record TaskListQuery() : IRequest<IApiResponse>
    {
        public string? Role { get; set; }
        public string? Name { get; set; }
    }
}
