using FMSEvaluering.Application.Commands;
using FMSEvaluering.Application.Commands.Interfaces;
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
