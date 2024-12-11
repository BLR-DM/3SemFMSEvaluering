using FMSExitSlip.Application.Services.ApplicationServiceInterface;
using FMSExitSlip.Application.Services.ProxyInterface;
using FMSExitSlip.Domain.Values.DataServer;

namespace FMSExitSlip.Application.Services;

public class StudentApplicationService : IStudentApplicationService
{
    private readonly IFmsDataProxy _fmsDataProxy;

    public StudentApplicationService(IFmsDataProxy fmsDataProxy)
    {
        _fmsDataProxy = fmsDataProxy;
    }

    async Task<StudentValue> IStudentApplicationService.GetStudentAsync(string userId)
    {
        var studentResultDto = await _fmsDataProxy.GetStudentAsync(userId);
        return new StudentValue
        {
            FirstName = studentResultDto.FirstName,
            LastName = studentResultDto.LastName,
            Email = studentResultDto.Email,
            Class = new ModelClassValue
            {
                Id = studentResultDto.Class.Id,
                Name = studentResultDto.Class.Name,
                TeacherSubjects = studentResultDto.Class.TeacherSubjects.Select(ts => new TeacherSubjectValue
                {
                    Id = ts.Id,
                    Lectures = ts.Lectures.Select(l => new LectureValue
                    {
                        Id = l.Id,
                        Title = l.Title,
                        Date = l.Date
                    }).ToList()
                }).ToList()
            },
            AppUserId = studentResultDto.AppUserId
        };
    }

    async Task<IEnumerable<StudentValue>> IStudentApplicationService.GetStudentsForLecture(string lectureId)
    {
        var studentResultDtos = await _fmsDataProxy.GetStudentsForLecture(lectureId);

        return studentResultDtos.Select(s => new StudentValue
        {
            AppUserId = s.AppUserId
        });

    }
}