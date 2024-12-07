using FMSExitSlip.Domain.Values.DataServer;

namespace FMSExitSlip.Application.Services
{
    public interface IStudentApplicationService
    {
        Task<StudentValue> GetStudentAsync(string studentId);
    }
}
