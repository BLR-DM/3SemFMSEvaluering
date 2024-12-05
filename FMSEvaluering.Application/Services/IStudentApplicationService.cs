using FMSEvaluering.Domain.Values.DataServer;

namespace FMSEvaluering.Application.Services
{
    public interface IStudentApplicationService
    {
        Task<StudentValue> GetStudentAsync(string studentId);
    }
}
