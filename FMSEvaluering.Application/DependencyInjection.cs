using FMSEvaluering.Application.Commands;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Services;
using FMSEvaluering.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPostCommand, PostCommand>();
        services.AddScoped<IForumCommand, ForumCommand>();
        services.AddScoped<IStudentDomainService, StudentDomainService>();
        services.AddScoped<ITeacherDomainService, TeacherDomainService>();

        return services;
    }
}