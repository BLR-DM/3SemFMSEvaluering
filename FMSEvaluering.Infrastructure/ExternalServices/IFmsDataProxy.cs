using FMSEvaluering.Domain.DomainServices;

namespace FMSEvaluering.Infrastructure.ExternalServices;

public interface IFmsDataProxy
{
    Task<StudentResultDto> GetStudentAsync(string appUserId);
    Task<TeacherResultDto> GetTeacherAsync(string appUserId);
}

public record StudentResultDto(string FirstName, string LastName, string Email, string ClassId, string AppUserId);
public record TeacherResultDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<TeacherSubjectResultDto> TeacherSubjects { get; set; }
    public string AppUserId { get; set; }
}

public record TeacherSubjectResultDto
{
    public string Id { get; set; }
    public string ClassId { get; set; }
    public string SubjectName { get; set; }
}