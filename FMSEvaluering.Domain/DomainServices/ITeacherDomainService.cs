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
        public string Id { get; set; }
        public ModelClassDto Class { get; set; }
        public IEnumerable<LectureDto> Lectures { get; set; }
    };

    public record LectureDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }

    public record ModelClassDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TeacherSubjectDto> TeacherSubjects { get; set; }
    }
}