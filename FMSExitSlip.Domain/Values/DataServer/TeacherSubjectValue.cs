namespace FMSExitSlip.Domain.Values.DataServer;

public record TeacherSubjectValue
{
    public string Id { get; set; }
    public ModelClassValue Class { get; set; }
    public SubjectValue Subject { get; set; }
    public IEnumerable<LectureValue> Lectures { get; set; }
};