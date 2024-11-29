﻿using FMSEvaluering.Application.ExternalServices;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Infrastructure.ExternalServices;
using FMSEvaluering.Infrastructure.Helpers;
using FMSEvaluering.Infrastructure.Queries;
using FMSEvaluering.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostQuery, PostQuery>();
        services.AddScoped<IForumRepository, ForumRepository>();
        services.AddScoped<IForumQuery, ForumQuery>();
        services.AddScoped<IUnitOfWork, UnitOfWork<EvaluationContext>>();

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