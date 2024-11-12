using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Infrastructure.Helpers;
using FMSEvaluering.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IEvaluationPostRepository, EvaluationPostRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork<EvaluationContext>>();

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
}