namespace FMSEvaluering.Domain.DomainServices
{
    public interface IStudentDomainService
    {
        Task<StudentDto> GetStudentAsync(string studentId);
    }

    public record StudentDto(string FirstName, string LastName, string Email, string ClassId, string AppUserId);
}
