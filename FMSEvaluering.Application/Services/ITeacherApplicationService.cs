using FMSEvaluering.Domain.Values.DataServer;

namespace FMSEvaluering.Application.Services
{
    public interface ITeacherApplicationService
    {
        Task<TeacherValue> GetTeacherAsync(string teacherId);
        Task<TeacherValue> GetTeacherForSubjectAsync(string teacherSubjectId);
        Task<IEnumerable<TeacherValue>> GetTeachersForClassAsync(string classId);
        Task<IEnumerable<TeacherValue>> GetTeachersAsync();

    }
}