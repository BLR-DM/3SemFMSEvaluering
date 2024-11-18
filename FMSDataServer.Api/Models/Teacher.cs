namespace FMSDataServer.Api.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TeacherSubject> TeacherSubjects { get; set; }
    }
}
