using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FMSUserService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            // Add-Migration InitialMigration -Context EvaluationContext -Project FMSEvaluering.DatabaseMigration
            // Update-Database -Context EvaluationContext -Project FMSEvaluering.DatabaseMigration

            services.AddDbContext<FMSIdentityContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString
                        ("FMSIdentityContext"),
                    x =>
                        x.MigrationsAssembly("FMSUserService.DatabaseMigration")));

            return services;
        }
    }
}
