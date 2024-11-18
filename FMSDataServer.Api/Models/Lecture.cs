namespace FMSDataServer.Api.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TeacherSubject TeacherSubject { get; set; }
    }
}
