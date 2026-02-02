using TaskManagementSystem.Domain.Interfaces;
//using TaskManagementSystem.Domain.Models;
using TaskManagementSystem.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Infrastructure.Repostiries
{
    public class RepoServices : IRepoServices
    {
        private readonly AppDbContext _appDbContext;
        public RepoServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> AddTask(TaskItem Task)
        {
            try
            {
                await _appDbContext.Tasks.AddAsync(Task);
                await _appDbContext.SaveChangesAsync();
                return "Task added";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<TaskItem?> GetTaskById(int id)
        {
            try
            {
                return await _appDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<List<TaskItem>> GetTaskByRole(string Name, string Role)
        {
            try
            {
                if (Role == "Admin")
                {
                    return await _appDbContext.Tasks.ToListAsync();
                }
                else
                {
                    return await _appDbContext.Tasks.Where(x => x.OwnerUserId == Name).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<string> UpdateStatus(int id, bool Status)
        {
            try
            {
                await _appDbContext.SaveChangesAsync();
                return "Task status updated successfully";
            }
            catch(Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task UpdateTask(TaskItem task)
        {
            try
            {
                _appDbContext.Tasks.Update(task);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
