namespace FMSEvaluering.Infrastructure.ExternalServices;

public interface IFmsDataProxy
{
    Task<StudentResultDto> GetStudentAsync(string appUserId);
    Task<TeacherResultDto> GetTeacherAsync(string appUserId);
}

public record StudentResultDto(string FirstName, string LastName, string Email, string ClassId, string AppUserId);
public record TeacherResultDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<TeacherSubject> TeacherSubjects { get; set; }
    public AppUser AppUser { get; set; }
}

public record TeacherSubjectResultDto(string Id, )