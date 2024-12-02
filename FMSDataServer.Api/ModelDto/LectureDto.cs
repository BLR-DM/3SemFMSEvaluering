namespace FMSDataServer.Api.ModelDto
{
    public record LectureDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public TeacherSubjectWithIdDto TeacherSubject { get; set; }
    }
}
