using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Infrastructure.Persistance;
using TaskManagementSystem.Infrastructure.Repostiries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Infrastructure.Configuarations
{
    public static class InfraConfig
    {
        public static void RepoConfig(this IServiceCollection services)
        {
            var cs = Environment.GetEnvironmentVariable("DbConnection");

            if (string.IsNullOrWhiteSpace(cs))
            {
                throw new InvalidOperationException("Database connection string 'DbConnection' is not set in environment variables.");
            }

            services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(cs, sqlOptions =>
              {
                  sqlOptions.EnableRetryOnFailure(
                      maxRetryCount: 3,
                      maxRetryDelay: TimeSpan.FromSeconds(5),
                      errorNumbersToAdd: null);
              }));

            services.AddScoped<IRepoServices, RepoServices>();

        }
    }
}
