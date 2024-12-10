using FMSExitSlip.Domain.Values.DataServer;

namespace FMSExitSlip.Application.Services.ApplicationServiceInterface
{
    public interface ITeacherApplicationService
    {
        Task<TeacherValue> GetTeacherAsync(string teacherId);
    }
}