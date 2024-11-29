using System.Net.Http.Json;
using FMSEvaluering.Application.ExternalServices;
using FMSEvaluering.Infrastructure.ExternalServices.Dto;

namespace FMSEvaluering.Infrastructure.ExternalServices;

public class FmsDataProxy : IFmsDataProxy
{
    private readonly HttpClient _httpclient;

    public FmsDataProxy(HttpClient httpclient)
    {
        _httpclient = httpclient;
    }

    async Task<string> IFmsDataProxy.GetStudentClassId(string studentId)
    {
        try
        {
            var student = await _httpclient.GetFromJsonAsync<StudentDto>($"/fms/student/{studentId}");

            return student is null ? string.Empty : student.ClassId;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}