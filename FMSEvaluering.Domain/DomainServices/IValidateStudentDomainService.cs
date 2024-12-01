namespace FMSEvaluering.Domain.DomainServices
{
    public interface IValidateStudentDomainService
    {
        Task<FmsValidationResult> ValidateUserAccess(string studentId);
    }

    public record FmsValidationResult(string FirstName, string LastName, string Email, string ClassId, string AppUserId);
}
