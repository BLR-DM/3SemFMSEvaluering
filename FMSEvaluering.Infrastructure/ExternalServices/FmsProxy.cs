using System.Net.Http.Json;
using FMSEvaluering.Infrastructure.ExternalServices.Dto;

namespace FMSEvaluering.Infrastructure.ExternalServices;

public class FmsProxy
{
    private readonly HttpClient _client;

    public FmsProxy(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> StudentIsPartOfClassroom(string studentId)
    {
        try
        {
            var requestUri = $"http://fmsdataserver.api:8080/fms/student/{studentId}";
            var student = await _client.GetFromJsonAsync<StudentDto>(requestUri);

            return student.ClassId;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }
}