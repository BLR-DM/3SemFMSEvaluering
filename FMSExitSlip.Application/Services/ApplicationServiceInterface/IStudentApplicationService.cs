using FMSExitSlip.Domain.Values.DataServer;

namespace FMSExitSlip.Application.Services.ApplicationServiceInterface
{
    public interface IStudentApplicationService
    {
        Task<StudentValue> GetStudentAsync(string studentId);
        Task<IEnumerable<StudentValue>> GetStudentsForLecture(string lectureId);
    }
}
