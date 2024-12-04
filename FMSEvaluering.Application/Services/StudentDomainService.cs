using FMSEvaluering.Application.Services.ProxyInterface;
using FMSEvaluering.Domain.DomainServices;

namespace FMSEvaluering.Application.Services;

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
        return new StudentDto
        {
            FirstName = studentResultDto.FirstName,
            LastName = studentResultDto.LastName,
            Email = studentResultDto.Email,
            Class = new ModelClassDto
            {
                Id = studentResultDto.Class.Id,
                Name = studentResultDto.Class.Name
            },
            AppUserId = studentResultDto.AppUserId
        };
    }
}