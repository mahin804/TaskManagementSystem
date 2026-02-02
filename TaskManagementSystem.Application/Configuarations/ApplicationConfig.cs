using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Application.Commands.Task_.TaskCmds;

namespace TaskManagementSystem.Application.Configuarations
{
    public static class ApplicationConfig
    {
        public static void MediatorRConfig(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(typeof(CreateTaskCmd).Assembly));
        }
    }
}
