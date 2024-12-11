using FMSEvaluering.Application.Commands;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPostCommand, PostCommand>();
        services.AddScoped<IForumCommand, ForumCommand>();
        services.AddScoped<IStudentApplicationService, StudentApplicationService>();
        services.AddScoped<ITeacherApplicationService, TeacherApplicationService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IGenerateCsvHandler, GenerateCsvHandler>();

        return services;
    }
}