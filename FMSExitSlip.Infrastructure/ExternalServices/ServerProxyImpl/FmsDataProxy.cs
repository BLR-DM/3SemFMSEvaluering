using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Services.ProxyInterface;

namespace FMSExitSlip.Infrastructure.ExternalServices.ServerProxyImpl
{
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
            catch (Exception)
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
    }
}
