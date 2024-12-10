using FMSExitSlip.Application.Services.ApplicationServiceInterface;
using FMSExitSlip.Application.Services.ProxyInterface;
using FMSExitSlip.Domain.Values.DataServer;

namespace FMSExitSlip.Application.Services;

public class TeacherApplicationService : ITeacherApplicationService
{
    private readonly IFmsDataProxy _fmsDataProxy;

    public TeacherApplicationService(IFmsDataProxy fmsDataProxy)
    {
        _fmsDataProxy = fmsDataProxy;
    }

    async Task<TeacherValue> ITeacherApplicationService.GetTeacherAsync(string userId)
    {
        var teacherResultDto = await _fmsDataProxy.GetTeacherAsync(userId);
        var teacherValue = new TeacherValue
        {
            AppUserId = teacherResultDto.AppUserId,
            FirstName = teacherResultDto.FirstName,
            LastName = teacherResultDto.LastName,
            Email = teacherResultDto.Email,
            TeacherSubjects = teacherResultDto.TeacherSubjects.Select(ts => new TeacherSubjectValue
            {
                Id = ts.Id,
                Class = new ModelClassValue
                {
                    Id = ts.Class.Id,
                    Name = ts.Class.Name
                }
            })
        };

        return teacherValue;
    }
}