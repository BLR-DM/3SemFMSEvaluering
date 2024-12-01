namespace FMSDataServer.Api.ModelDto
{
    public record TeacherSubjectDto
    {
        public string Id { get; set; }
        public string ClassId { get; set; }
        public string SubjectName { get; set; }
    }
}
