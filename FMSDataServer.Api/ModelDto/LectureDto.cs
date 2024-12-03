namespace FMSDataServer.Api.ModelDto
{
    public record LectureDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TeacherSubjectDto TeacherSubject { get; set; }
    }
}
