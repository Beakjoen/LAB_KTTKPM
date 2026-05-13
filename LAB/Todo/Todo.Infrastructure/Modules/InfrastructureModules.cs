using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.Repositories;
using ToDo.Domain.Repositories;

namespace Todo.Infrastructure.Modules
{
    public static class InfrastructureModules
    {
        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
            return services;
        }
    }
}
