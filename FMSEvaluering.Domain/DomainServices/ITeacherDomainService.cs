namespace FMSEvaluering.Domain.DomainServices
{
    public interface ITeacherDomainService
    {
        Task<TeacherDto> GetTeacherAsync(string teacherId);
    }

    public record TeacherDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<TeacherSubjectDto> TeacherSubjects { get; set; }
        public string AppUserId { get; set; }
    };

    public record TeacherSubjectDto
    {
        public string ClassId { get; set; }
        public string SubjectName { get; set; }
    };
}