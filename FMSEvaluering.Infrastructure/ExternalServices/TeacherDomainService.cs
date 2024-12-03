using FMSEvaluering.Domain.DomainServices;

namespace FMSEvaluering.Infrastructure.ExternalServices;

public class TeacherDomainService : ITeacherDomainService
{
    private readonly IFmsDataProxy _fmsDataProxy;

    public TeacherDomainService(IFmsDataProxy fmsDataProxy)
    {
        _fmsDataProxy = fmsDataProxy;
    }

    async Task<TeacherDto> ITeacherDomainService.GetTeacherAsync(string userId)
    {
        var teacherResultDto = await _fmsDataProxy.GetTeacherAsync(userId);
        var teacherDto = new TeacherDto
        {
            AppUserId = teacherResultDto.AppUserId,
            FirstName = teacherResultDto.FirstName,
            LastName = teacherResultDto.LastName,
            Email = teacherResultDto.Email,
            TeacherSubjects = teacherResultDto.TeacherSubjects.Select(ts => new TeacherSubjectDto
            {
                Id = ts.Id,
                Class = new ModelClassDto
                {
                    Id = ts.Class.Id,
                    Name = ts.Class.Name
                }
            })
        };

        return teacherDto;
    }
}