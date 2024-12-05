namespace FMSEvaluering.Domain.Values.DataServer;

public record TeacherSubjectValue
{
    public string Id { get; set; }
    public ModelClassValue Class { get; set; }
    public IEnumerable<LectureValue> Lectures { get; set; }
};