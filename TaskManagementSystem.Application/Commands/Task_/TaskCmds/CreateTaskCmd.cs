using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Response;

namespace TaskManagementSystem.Application.Commands.Task_.TaskCmds
{
    public record CreateTaskCmd() : IRequest<IApiResponse>
    {
        public int Id { get; init; }
        public string Title { get; init; } = default!;
        public string? Description { get; init; }
        public DateTime? DueDate { get; init; }
        public string? OwnerUserId { get; set; }
    }
}
