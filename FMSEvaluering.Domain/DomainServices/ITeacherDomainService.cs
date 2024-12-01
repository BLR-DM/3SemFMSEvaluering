namespace FMSEvaluering.Domain.DomainServices
{
    public interface ITeacherDomainService
    {
        Task<TeacherDto> GetTeacherAsync(string teacherId);
    }

    public record TeacherDto(string FirstName, string LastName, string Email, IEnumerable<TeacherSubjectDto> TeacherSubjects, string AppUserId);

    public record TeacherSubjectDto(string ClassId);
}