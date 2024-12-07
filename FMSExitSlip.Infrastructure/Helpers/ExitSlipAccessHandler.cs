using FMSExitSlip.Application.Helpers;
using FMSExitSlip.Application.Services;
using FMSExitSlip.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace FMSExitSlip.Infrastructure.Helpers;

public class ExitSlipAccessHandler : IExitSlipAccessHandler
{
    private readonly IServiceProvider _serviceProvider;

    public ExitSlipAccessHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    async Task IExitSlipAccessHandler.ValidateExitslipAccess(string appUserId, string role, ExitSlip exitSlip)
    {
        if (role == "student")
        {
            var studentApplicationService = _serviceProvider.GetRequiredService<IStudentApplicationService>();
            var studentDto = await studentApplicationService.GetStudentAsync(appUserId);

            exitSlip.ValidateStudentAccess(studentDto);
        }
        else if (role == "teacher")
        {
            var teacherApplicationService = _serviceProvider.GetRequiredService<ITeacherApplicationService>();
            var teacherDto = await teacherApplicationService.GetTeacherAsync(appUserId);

            exitSlip.ValidateTeacherAccess(teacherDto);
        }
    }
}