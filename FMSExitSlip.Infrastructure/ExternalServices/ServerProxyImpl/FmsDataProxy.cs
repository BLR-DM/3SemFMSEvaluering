using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace FMSExitSlip.Infrastructure.ExternalServices.ServerProxyImpl
{
    public class FmsDataProxy : IFmsDataProxy
    {
        private readonly HttpClient _client;

        public FmsDataProxy(HttpClient client)
        {
            _client = client;
        }

        async Task<LectureResultDto> IFmsDataProxy.GetLectureAsync(string lectureId)
        {
            try
            {
                var lectureResult =
                    await _client.GetFromJsonAsync<LectureResultDto>($"/lecture/{lectureId}");

                if (lectureResult == null)
                    throw new InvalidOperationException("Lecture not found");

                return lectureResult;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
