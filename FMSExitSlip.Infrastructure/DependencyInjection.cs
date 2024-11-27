using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Helpers;
using FMSExitSlip.Application.Repositories;
using FMSExitSlip.Infrastructure.Helpers;
using FMSExitSlip.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FMSExitSlip.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<ExitSlipContext>>();
            services.AddScoped<IExitSlipRepository, ExitSlipRepository>();
            // Add-Migration InitialMigration -Context ExitSlipContext -Project FMSExitSlip.DatabaseMigration
            // Update-Database -Context ExitSlipContext -Project FMSExitSlip.DatabaseMigration

            services.AddDbContext<ExitSlipContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString
                        ("ExitSlipDbConnection"),
                    x => 
                        x.MigrationsAssembly("FMSExitSlip.DatabaseMigration")));
            return services;
        }
    }
}
