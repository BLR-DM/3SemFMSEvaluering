using FMSExitSlip.Domain.Values.DataServer;

namespace FMSExitSlip.Application.Services
{
    public interface ITeacherApplicationService
    {
        Task<TeacherValue> GetTeacherAsync(string teacherId);
    }
}