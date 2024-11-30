namespace FMSEvaluering.Infrastructure.ExternalServices;

public interface IFmsDataProxy
{
    Task<FmsValidationResultDto> GetStudentAsync(string appUserId);
}

public record FmsValidationResultDto(string FirstName, string LastName, string Email, string ClassId, string AppUserId);