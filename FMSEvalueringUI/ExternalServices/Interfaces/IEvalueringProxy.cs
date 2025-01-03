using FMSEvalueringUI.ModelDto.FMSEvaluering.QueryDto;

namespace FMSEvalueringUI.ExternalServices.Interfaces
{
    public interface IEvalueringProxy
    {
        Task<List<ForumDto>> GetForumsAsync();
        Task<ForumDto> GetPostsAsync(string forumId);
        Task<ForumDto> GetPostAsync(string forumId, string postId);
    }
}
