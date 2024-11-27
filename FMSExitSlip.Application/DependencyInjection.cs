using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Commands;
using FMSExitSlip.Application.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FMSExitSlip.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IExitSlipCommand, ExitSlipCommand>();

            return services;
        }
    }
}
