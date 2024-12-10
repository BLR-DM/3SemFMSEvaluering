using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Commands;
using FMSExitSlip.Application.Commands.Interfaces;
using FMSExitSlip.Application.Services;
using FMSExitSlip.Application.Services.ApplicationServiceInterface;
using Microsoft.Extensions.DependencyInjection;

namespace FMSExitSlip.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IExitSlipCommand, ExitSlipCommand>();
            services.AddScoped<IStudentApplicationService, StudentApplicationService>();
            services.AddScoped<ITeacherApplicationService, TeacherApplicationService>();
            services.AddScoped<IExitSlipGenerator, ExitSlipGenerator>();
            services.AddScoped<ILectureApplicationService, LectureApplicationService>();

            return services;
        }
    }
}
