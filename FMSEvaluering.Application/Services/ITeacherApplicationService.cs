using FMSEvaluering.Domain.Values.DataServer;

namespace FMSEvaluering.Application.Services
{
    public interface ITeacherApplicationService
    {
        Task<TeacherValue> GetTeacherAsync(string teacherId);
    }
}