using System.Net.Http.Json;
using FMSEvaluering.Infrastructure.ExternalServices.Dto;

namespace FMSEvaluering.Infrastructure.ExternalServices;

public class FmsProxy : IFmsProxy
{
    private readonly HttpClient _client;

    public FmsProxy(HttpClient client)
    {
        _client = client;
    }

    async Task<string> IFmsProxy.StudentIsPartOfClassroom(string studentId)
    {
        try
        {
            var requestUri = $"http://fmsdataserver.api:8080/fms/student/{studentId}";
            var student = await _client.GetFromJsonAsync<StudentDto>(requestUri);

            return student is null ? string.Empty : student.ClassId;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}