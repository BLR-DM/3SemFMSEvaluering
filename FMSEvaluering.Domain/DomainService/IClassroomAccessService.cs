namespace FMSEvaluering.Domain.DomainService
{
    public interface IClassroomAccessService
    {
        Task<string> GetStudentClassId(string studentId);
    }
}
