using FMSEvalueringUI.ExternalServices.Interfaces;
using FMSEvalueringUI.ModelDto;

namespace FMSEvalueringUI.ExternalServices
{
    public class EvalueringProxy : IEvalueringProxy
    {
        private readonly HttpClient _httpClient;

        public EvalueringProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        async Task<List<ForumDto>> IEvalueringProxy.GetForumsAsync()
        {
            var requestUri = "/forum";



            var forums = await _httpClient.GetFromJsonAsync<List<ForumDto>>("/evaluation/forum");
            return forums;
        }

    }
}
