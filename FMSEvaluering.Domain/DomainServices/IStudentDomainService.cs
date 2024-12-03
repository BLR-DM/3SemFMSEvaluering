namespace FMSEvaluering.Domain.DomainServices
{
    public interface IStudentDomainService
    {
        Task<StudentDto> GetStudentAsync(string studentId);
    }

    public record StudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ModelClassDto Class { get; set; }
        public string AppUserId { get; set; }
    }
}
