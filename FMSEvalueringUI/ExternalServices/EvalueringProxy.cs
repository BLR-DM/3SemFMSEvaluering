using FMSEvalueringUI.ExternalServices.Interfaces;
using FMSEvalueringUI.ModelDto;
using System.Net.Http.Headers;
using FMSEvalueringUI.Services;

namespace FMSEvalueringUI.ExternalServices
{
    public class EvalueringProxy : IEvalueringProxy
    {
        private readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;

        public EvalueringProxy(HttpClient httpClient, IServiceProvider serviceProvider)
        {
            _httpClient = httpClient;
            _serviceProvider = serviceProvider;
        }
        async Task<List<ForumDto>> IEvalueringProxy.GetForumsAsync()
        {
            var token = await _serviceProvider.GetRequiredService<IAuthService>().GetJwtTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var forums = await _httpClient.GetFromJsonAsync<List<ForumDto>>("/evaluation/forum");
            return forums;
        }

        async Task<ForumDto> IEvalueringProxy.GetPostsAsync(string forumId)
        {
            var token = await _serviceProvider.GetRequiredService<IAuthService>().GetJwtTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var forum = await _httpClient.GetFromJsonAsync<ForumDto>($"evaluation/forum/{int.Parse(forumId)}/posts");
            return forum;
        }
    }
}
