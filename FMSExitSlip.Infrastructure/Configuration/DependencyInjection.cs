using FMSExitSlip.Application.Helpers;
using FMSExitSlip.Application.Queries.Interfaces;
using FMSExitSlip.Application.Repositories;
using FMSExitSlip.Application.Services;
using FMSExitSlip.Application.Services.ProxyInterface;
using FMSExitSlip.Infrastructure.ExternalServices.ServerProxyImpl;
using FMSExitSlip.Infrastructure.Helpers;
using FMSExitSlip.Infrastructure.Queries;
using FMSExitSlip.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FMSExitSlip.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<ExitSlipContext>>();
            services.AddScoped<IExitSlipRepository, ExitSlipRepository>();
            services.AddScoped<IExitSlipQuery, ExitSlipQuery>();
            services.AddScoped<ITeacherApplicationService, TeacherApplicationService>(); // Test
            services.AddScoped<IExitSlipAccessHandler, ExitSlipAccessHandler>();
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
