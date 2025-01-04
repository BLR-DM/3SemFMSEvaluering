using FMSEvalueringUI.ExternalServices.Interfaces;
using System.Net.Http.Headers;
using FMSEvalueringUI.ModelDto.FMSEvaluering.CommandDto.CommentDto;
using FMSEvalueringUI.ModelDto.FMSEvaluering.CommandDto.PostDto;
using FMSEvalueringUI.ModelDto.FMSEvaluering.CommandDto.VoteDto;
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

        async Task IEvalueringProxy.HandleVote(string forumId, string postId, HandleVoteDto vote)
        {
            var token = await _serviceProvider.GetRequiredService<IAuthService>().GetJwtTokenAsync();

            if (token == null)
                throw new UnauthorizedAccessException("Unauthorized request");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var requestUri = $"evaluation/forum/{forumId}/post/{postId}/vote";

            var response = await _httpClient.PostAsJsonAsync(requestUri, vote);
            if (!response.IsSuccessStatusCode)
            {
                //return Results.Problem("Failed to authenticate user.", statusCode: (int)response.StatusCode);
            }
        }

        async Task IEvalueringProxy.CreatePost(string forumId, CreatePostDto post)
        {
            var token = await _serviceProvider.GetRequiredService<IAuthService>().GetJwtTokenAsync();

            if (token == null)
                throw new UnauthorizedAccessException("Unauthorized request");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var requestUri = $"evaluation/forum/{forumId}/post";

            var response = await _httpClient.PostAsJsonAsync(requestUri, post);
            if (!response.IsSuccessStatusCode)
            {
                //return Results.Problem("Failed to authenticate user.", statusCode: (int)response.StatusCode);
            }
        }

        async Task IEvalueringProxy.CreateComment(string forumId, string postId, CreateCommentDto comment)
        {
            var token = await _serviceProvider.GetRequiredService<IAuthService>().GetJwtTokenAsync();

            if (token == null)
                throw new UnauthorizedAccessException("Unauthorized request");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var requestUri = $"evaluation/forum/{forumId}/post/{postId}/comment";

            var response = await _httpClient.PostAsJsonAsync(requestUri, comment);
            if (!response.IsSuccessStatusCode)
            {
                //return Results.Problem("Failed to authenticate user.", statusCode: (int)response.StatusCode);
            }
        }

        async Task IEvalueringProxy.UpdatePost(string forumId, string postId, UpdatePostDto post)
        {
            var token = await _serviceProvider.GetRequiredService<IAuthService>().GetJwtTokenAsync();

            if (token == null)
                throw new UnauthorizedAccessException("Unauthorized request");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var requestUri = $"evaluation/forum/{forumId}/post/{postId}";

            var response = await _httpClient.PutAsJsonAsync(requestUri, post);
            if (!response.IsSuccessStatusCode)
            {
                //return Results.Problem("Failed to authenticate user.", statusCode: (int)response.StatusCode);
            }
        }

        async Task IEvalueringProxy.UpdateComment(string forumId, string postId, string commentId, UpdateCommentDto comment)
        {
            var token = await _serviceProvider.GetRequiredService<IAuthService>().GetJwtTokenAsync();

            if (token == null)
                throw new UnauthorizedAccessException("Unauthorized request");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var requestUri = $"evaluation/forum/{forumId}/post/{postId}/comment/{commentId}";

            var response = await _httpClient.PutAsJsonAsync(requestUri, comment);
            if (!response.IsSuccessStatusCode)
            {
                //return Results.Problem("Failed to authenticate user.", statusCode: (int)response.StatusCode);
            }
        }
    }
}
