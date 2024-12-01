using FMSEvaluering.Domain.DomainServices;

namespace FMSEvaluering.Infrastructure.ExternalServices;

public class ValidateStudentDomainService : IValidateStudentDomainService
{
    private readonly IFmsDataProxy _fmsDataProxy;

    public ValidateStudentDomainService(IFmsDataProxy fmsDataProxy)
    {
        _fmsDataProxy = fmsDataProxy;
    }

    async Task<FmsValidationResult> IValidateStudentDomainService.ValidateUserAccess(string studentId)
    {
        var result = await _fmsDataProxy.GetStudentAsync(studentId);
        return new FmsValidationResult(result.FirstName, result.LastName, result.Email, result.ClassId, result.AppUserId);
    }
}