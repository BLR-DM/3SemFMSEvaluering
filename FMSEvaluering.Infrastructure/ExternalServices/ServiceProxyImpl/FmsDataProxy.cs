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
            var studentResult = await _httpClient.GetFromJsonAsync<StudentResultDto>($"/student/{appUserId}");

            if (studentResult is null)
                throw new InvalidOperationException("Student not found");

            return studentResult;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Something went wrong with FmsDataProxy");
        }
    }

    async Task<TeacherResultDto> IFmsDataProxy.GetTeacherAsync(string appUserId)
    {
        try
        {
            var teacherResult = await _httpClient.GetFromJsonAsync<TeacherResultDto>($"/teacher/{appUserId}");

            if (teacherResult is null)
                throw new InvalidOperationException("Teacher not found");

            return teacherResult;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Something went wrong with FmsDataProxy");
        }
    }

    async Task<TeacherResultDto> IFmsDataProxy.GetTeacherForSubjectAsync(string teacherSubjectId)
    {
        try
        {
            var teacherResult = await _httpClient.GetFromJsonAsync<TeacherResultDto>($"/teachersubject/{teacherSubjectId}/teacher");

            if (teacherResult is null)
                throw new InvalidOperationException("Teacher not found");

            return teacherResult;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Something went wrong with FmsDataProxy");
        }
    }

    async Task<IEnumerable<TeacherResultDto>> IFmsDataProxy.GetTeachersAsync()
    {
        try
        {
            var teacherResult = await _httpClient.GetFromJsonAsync<IEnumerable<TeacherResultDto>>($"/teachers");

            if (teacherResult is null)
                throw new InvalidOperationException("No teachers found");

            return teacherResult;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Something went wrong with FmsDataProxy");
        }
    }

    async Task<IEnumerable<TeacherResultDto>> IFmsDataProxy.GetTeachersForClassAsync(string classId)
    {
        try
        {
            var teacherResult = await _httpClient.GetFromJsonAsync<IEnumerable<TeacherResultDto>>($"/class/{classId}/teachers");

            if (teacherResult is null)
                throw new InvalidOperationException("No teachers found");

            return teacherResult;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Something went wrong with FmsDataProxy");
        }
    }
}