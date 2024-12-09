using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.MailService;
using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Application.Services.ProxyInterface;
using FMSEvaluering.Infrastructure.ExternalServices.ServiceProxyImpl;
using FMSEvaluering.Infrastructure.Helpers;
using FMSEvaluering.Infrastructure.Helpers.Interfaces;
using FMSEvaluering.Infrastructure.MailService;
using FMSEvaluering.Infrastructure.Queries;
using FMSEvaluering.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IForumRepository, ForumRepository>();
        services.AddScoped<IForumQuery, ForumQuery>();
        services.AddScoped<IUnitOfWork, UnitOfWork<EvaluationContext>>();
        services.AddScoped<IForumMapper, ForumMapper>();
        services.AddScoped<IForumAccessHandler, ForumAccessHandler>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IMail, Mail>();

        // External services
        services.AddHttpClient<IFmsDataProxy, FmsDataProxy>(client =>
        {
            client.BaseAddress = new Uri(configuration["FmsDataProxy:BaseAddress"]);
        });

        // Add-Migration InitialMigration -Context EvaluationContext -Project FMSEvaluering.DatabaseMigration
        // Update-Database -Context EvaluationContext -Project FMSEvaluering.DatabaseMigration

        services.AddDbContext<EvaluationContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString
                    ("EvaluationDbConnection"),
                x =>
                    x.MigrationsAssembly("FMSEvaluering.DatabaseMigration")));

        return services;
    }
}