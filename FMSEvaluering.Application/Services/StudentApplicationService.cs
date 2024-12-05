using FMSEvaluering.Application.Services.ProxyInterface;
using FMSEvaluering.Domain.Values.DataServer;

namespace FMSEvaluering.Application.Services;

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
                Name = studentResultDto.Class.Name
            },
            AppUserId = studentResultDto.AppUserId
        };
    }
}