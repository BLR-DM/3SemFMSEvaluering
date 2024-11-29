using System.Net.Http.Json;

namespace FMSEvaluering.Infrastructure.ExternalServices.ServiceProxyImpl;

public class FmsDataProxy : IFmsDataProxy
{
    private readonly HttpClient _client;

    public FmsDataProxy(HttpClient client)
    {
        _client = client;
    }

    async Task<FmsValidationResultDto> IFmsDataProxy.GetStudentAsync(string appUserId)
    {
        try
        {
            var studentDto = await _client.GetFromJsonAsync<FmsValidationResultDto>($"/fms/student/{appUserId}");

            if (studentDto is null)
                throw new InvalidOperationException("Student not found");

            return studentDto;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Something went wrong with FmsDataProxy");
            ;
        }
    }
}