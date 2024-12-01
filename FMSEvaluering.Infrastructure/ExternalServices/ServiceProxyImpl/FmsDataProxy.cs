using System.Net.Http.Json;

namespace FMSEvaluering.Infrastructure.ExternalServices.ServiceProxyImpl;

public class FmsDataProxy : IFmsDataProxy
{
    private readonly HttpClient _client;

    public FmsDataProxy(HttpClient client)
    {
        _client = client;
    }

    async Task<StudentResultDto> IFmsDataProxy.GetStudentAsync(string appUserId)
    {
        try
        {
            var studentResult = await _client.GetFromJsonAsync<StudentResultDto>($"/fms/student/{appUserId}");

            if (studentResult is null)
                throw new InvalidOperationException("Student not found");

            return studentResult;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Something went wrong with FmsDataProxy");
        }
    }

    async Task<TeacherResultDto>
}