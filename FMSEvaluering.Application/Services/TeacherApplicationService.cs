using FMSEvaluering.Application.Services.ProxyInterface;
using FMSEvaluering.Domain.Values.DataServer;

namespace FMSEvaluering.Application.Services;

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