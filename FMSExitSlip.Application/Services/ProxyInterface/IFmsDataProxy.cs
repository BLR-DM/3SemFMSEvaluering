using FMSExitSlip.Domain.Values.DataServer;

namespace FMSExitSlip.Application.Services.ProxyInterface
{
    public interface IFmsDataProxy
    {
        Task<StudentResultDto> GetStudentAsync(string studentId);
        Task<TeacherResultDto> GetTeacherAsync(string teacherId);
        Task<IEnumerable<StudentResultDto>> GetStudentsForLecture(string lectureId);
    }

    public record StudentResultDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ClassResultDto Class { get; set; }
        public string AppUserId { get; set; }
    }

    public record ClassResultDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<StudentResultDto> Students { get; set; }
        public List<TeacherSubjectResultDto> TeacherSubjectResults { get; set; }
    }

    public record TeacherResultDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<TeacherSubjectResultDto> TeacherSubjects { get; set; }
        public string AppUserId { get; set; }
    }

    public record TeacherSubjectResultDto
    {
        public string Id { get; set; }
        public SubjectResultDto Subject { get; set; }
        public ClassResultDto Class { get; set; }
        public List<LectureResultDto> Lectures { get; set; }
    }

    public record SubjectResultDto()
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TeacherSubjectResultDto> TeacherSubjects { get; set; }
    }

    public record LectureResultDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}
