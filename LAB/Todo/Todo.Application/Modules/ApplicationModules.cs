using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Application.Modules
{
    public static class ApplicationModules
    {
        public static IServiceCollection AddApplicationModules(this IServiceCollection services)
        {
            services.AddScoped<Services.ITodoService, Services.TodoService>();
            return services;
        }
    }
}
