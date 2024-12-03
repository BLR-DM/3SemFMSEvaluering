using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Helpers;
using FMSExitSlip.Application.Repositories;
using FMSExitSlip.Domain.DomainServices;
using FMSExitSlip.Infrastructure.ExternalServices;
using FMSExitSlip.Infrastructure.ExternalServices.ServerProxyImpl;
using FMSExitSlip.Infrastructure.Helpers;
using FMSExitSlip.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FMSExitSlip.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<ExitSlipContext>>();
            services.AddScoped<IExitSlipRepository, ExitSlipRepository>();
            services.AddScoped<ILectureDomainService, LectureDomainService>();
            services.AddHttpClient<IFmsDataProxy, FmsDataProxy>(client =>
            {
                client.BaseAddress = new Uri(configuration["FmsDataProxy:BaseAddress"]);
            });


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
