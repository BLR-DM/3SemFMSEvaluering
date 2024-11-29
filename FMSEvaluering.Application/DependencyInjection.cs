using FMSEvaluering.Application.Commands;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Service;
using FMSEvaluering.Domain.DomainService;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPostCommand, PostCommand>();
        services.AddScoped<IForumCommand, ForumCommand>();
        services.AddScoped<IClassroomAccessService, ClassroomAccessService>();

        return services;
    }
}