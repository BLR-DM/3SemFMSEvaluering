namespace FMSEvaluering.Application.Services.ProxyInterface;

public interface IFmsDataProxy
{
    Task<StudentResultDto> GetStudentAsync(string appUserId);
    Task<TeacherResultDto> GetTeacherAsync(string appUserId);
    Task<TeacherResultDto> GetTeacherForSubjectAsync(string teacherSubjectId);
    Task<IEnumerable<TeacherResultDto>> GetTeachersAsync();
    Task<IEnumerable<TeacherResultDto>> GetTeachersForClassAsync(string classId);
}

public record StudentResultDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public ClassResultDto Class { get; set; }
    public string AppUserId { get; set; }
}

public record ClassResultDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<TeacherSubjectResultDto> TeacherSubjectResults { get; set; }
}

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

    public ClassResultDto Class { get; set; }
    List<LectureResultDto> Lectures { get; set; }

}

public record LectureResultDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
}