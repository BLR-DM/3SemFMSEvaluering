namespace FMSDataServer.Api.ModelDto
{
    public record TeacherSubjectDto
    {
        public string Id { get; set; }
        public List<LectureDto> Lectures { get; set; }
        public TeacherDto Teacher { get; set; }
        public ModelClassDto Class { get; set; }
    }
}
