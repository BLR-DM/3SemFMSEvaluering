using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEvaluationPostCommand, EvaluationPostCommand>();

            return services;
        }
    }
}
