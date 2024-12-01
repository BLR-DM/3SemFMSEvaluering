using FMSEvaluering.Domain.DomainServices;

namespace FMSEvaluering.Infrastructure.ExternalServices;

public class StudentDomainService : IStudentDomainService
{
    private readonly IFmsDataProxy _fmsDataProxy;

    public StudentDomainService(IFmsDataProxy fmsDataProxy)
    {
        _fmsDataProxy = fmsDataProxy;
    }

    async Task<StudentDto> IStudentDomainService.GetStudentAsync(string userId)
    {
        var studentResultDto = await _fmsDataProxy.GetStudentAsync(userId);
        return new StudentDto(studentResultDto.FirstName, studentResultDto.LastName, studentResultDto.Email, studentResultDto.ClassId, studentResultDto.AppUserId);
    }
}