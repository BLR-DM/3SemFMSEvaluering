using System.Net.Http.Headers;
using Blazored.LocalStorage;
using FMSEvalueringUI.ExternalServices.Interfaces;
using FMSEvalueringUI.ModelDto;

namespace FMSEvalueringUI.ExternalServices
{
    public class EvalueringProxy : IEvalueringProxy
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public EvalueringProxy(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        async Task<List<ForumDto>> IEvalueringProxy.GetForumsAsync()
        {
            
            var token = DataServerProxy.JwtToken;
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://gateway.api:8080/evaluation/forum");
            if (!string.IsNullOrEmpty(token))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var forums = await response.Content.ReadFromJsonAsync<List<ForumDto>>();
                Console.WriteLine("hej");
                return forums;
            }

            return new List<ForumDto>();
        }

    }
}
