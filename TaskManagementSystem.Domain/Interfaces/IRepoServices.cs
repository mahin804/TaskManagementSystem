using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Domain.Interfaces
{
    public interface IRepoServices
    {
        Task<string> AddTask(TaskItem user);
        Task UpdateTask(TaskItem task);
        Task<TaskItem?> GetTaskById(int id);
        Task<List<TaskItem>> GetTaskByRole(string Name,string Role);
        Task<string> UpdateStatus(int id, bool Status);
    }
}
