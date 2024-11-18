namespace FMSDataServer.Api.Models
{
    public class ModelClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public List<TeacherSubject> TeacherSubjects { get; set; }
    }
}
