namespace FMSExitSlip.Domain.Values.DataServer;

public record SubjectValue()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TeacherSubjectValue> TeacherSubjects { get; set; }
}