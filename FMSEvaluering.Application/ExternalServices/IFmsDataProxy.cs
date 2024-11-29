namespace FMSEvaluering.Application.ExternalServices
{
    public interface IFmsDataProxy
    {
        Task<string> GetStudentClassId(string studentId);
    }
}
