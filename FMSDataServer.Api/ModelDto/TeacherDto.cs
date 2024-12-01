namespace FMSDataServer.Api.ModelDto
{
    public record TeacherDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  Email { get; set; }
        public List<TeacherSubjectDto> TeacherSubjects { get; set; }
        public string AppUserId { get; set; }
        
    }
}
