using FMSEvalueringUI.ExternalServices.Interfaces;
using System.Net.Http.Headers;
using FMSEvalueringUI.ModelDto.FMSEvaluering.QueryDto;
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

            if (token == null)
                throw new UnauthorizedAccessException("Unauthorized request");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var forums = await _httpClient.GetFromJsonAsync<List<ForumDto>>("/evaluation/forum");
            return forums;
        }

        async Task<ForumDto> IEvalueringProxy.GetPostsAsync(string forumId)
        {
            var token = await _serviceProvider.GetRequiredService<IAuthService>().GetJwtTokenAsync();

            if (token == null)
                throw new UnauthorizedAccessException("Unauthorized request");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var forum = await _httpClient.GetFromJsonAsync<ForumDto>($"evaluation/forum/{int.Parse(forumId)}/posts");
            return forum;
        }

        async Task<ForumDto> IEvalueringProxy.GetPostAsync(string forumId, string postId)
        {
            var token = await _serviceProvider.GetRequiredService<IAuthService>().GetJwtTokenAsync();

            if (token == null)
                throw new UnauthorizedAccessException("Unauthorized request");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var forum = await _httpClient.GetFromJsonAsync<ForumDto>
                ($"evaluation/forum/{int.Parse(forumId)}/post/{int.Parse(postId)}");
            return forum;
        }
    }
}
