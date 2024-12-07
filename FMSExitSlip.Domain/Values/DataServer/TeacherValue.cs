namespace FMSExitSlip.Domain.Values.DataServer;

public record TeacherValue
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public IEnumerable<TeacherSubjectValue> TeacherSubjects { get; set; }
    public string AppUserId { get; set; }
};