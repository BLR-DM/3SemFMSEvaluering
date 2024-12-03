namespace FMSDataServer.Api.ModelDto
{
    public class ModelClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TeacherSubjectDto> TeacherSubjects { get; set; }
        public List<StudentDto> Students { get; set; }
    }
}
