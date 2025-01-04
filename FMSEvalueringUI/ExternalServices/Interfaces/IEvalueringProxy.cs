using FMSEvalueringUI.ModelDto.FMSEvaluering.CommandDto.CommentDto;
using FMSEvalueringUI.ModelDto.FMSEvaluering.CommandDto.PostDto;
using FMSEvalueringUI.ModelDto.FMSEvaluering.CommandDto.VoteDto;
using FMSEvalueringUI.ModelDto.FMSEvaluering.QueryDto;

namespace FMSEvalueringUI.ExternalServices.Interfaces
{
    public interface IEvalueringProxy
    {
        Task<List<ForumDto>> GetForumsAsync();
        Task<ForumDto> GetPostsAsync(string forumId);
        Task<ForumDto> GetPostAsync(string forumId, string postId);
        Task HandleVote(string forumId, string postId, HandleVoteDto vote);
        Task CreatePost(string forumId, CreatePostDto post);
        Task CreateComment(string forumId, string postId, CreateCommentDto comment);
        Task UpdatePost(string forumId, string postId, UpdatePostDto post);
    }
}
