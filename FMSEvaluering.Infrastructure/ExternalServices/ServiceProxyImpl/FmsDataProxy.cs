using System.Net.Http.Json;
using FMSEvaluering.Application.Services.ProxyInterface;

namespace FMSEvaluering.Infrastructure.ExternalServices.ServiceProxyImpl;

public class FmsDataProxy : IFmsDataProxy
{
    private readonly HttpClient _httpClient;

    public FmsDataProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    async Task<StudentResultDto> IFmsDataProxy.GetStudentAsync(string appUserId)
    {
        try
        {
            var studentResult = await _httpClient.GetFromJsonAsync<StudentResultDto>($"/fms/student/{appUserId}");

            if (studentResult is null)
                throw new InvalidOperationException("Student not found");

            return studentResult;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Something went wrong with FmsDataProxy");
        }
    }

    async Task<TeacherResultDto> IFmsDataProxy.GetTeacherAsync(string appUserId)
    {
        try
        {
            var teacherResult = await _httpClient.GetFromJsonAsync<TeacherResultDto>($"/fms/teacher/{appUserId}");

            if (teacherResult is null)
                throw new InvalidOperationException("Teacher not found");

            return teacherResult;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Something went wrong with FmsDataProxy");
        }
    }
}