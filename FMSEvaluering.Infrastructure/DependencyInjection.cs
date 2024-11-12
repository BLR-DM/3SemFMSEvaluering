using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // Add-Migration InitialMigration -Context contextnavnet -Project FMSEvaluering.DatabaseMigration
            // Update-Database -Context contectnavnet -Project FMSEvaluering.DatabaseMigration

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