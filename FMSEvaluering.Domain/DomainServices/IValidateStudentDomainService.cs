namespace FMSEvaluering.Domain.DomainServices
{
    public interface IValidateStudentDomainService
    {
        Task<FmsValidationResult> ValidateStudent(string studentId);
    }

    public record FmsValidationResult(string FirstName, string LastName, string Email, string ClassId, string AppUserId);
}
