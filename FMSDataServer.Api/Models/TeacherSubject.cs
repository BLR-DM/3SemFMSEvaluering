namespace FMSDataServer.Api.Models
{
    public class TeacherSubject
    {
        public int Id { get; set; }
        public ModelClass Class { get; set; }
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public List<Lecture> Lectures { get; set; }
    }
}
