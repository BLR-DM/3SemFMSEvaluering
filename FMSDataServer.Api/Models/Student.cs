namespace FMSDataServer.Api.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ModelClass Class { get; set; }
    }
}
