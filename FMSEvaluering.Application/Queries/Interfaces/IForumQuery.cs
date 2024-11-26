using FMSEvaluering.Application.Queries.QueryDto;

namespace FMSEvaluering.Application.Queries.Interfaces;

public interface IForumQuery
{
    Task<ForumDto> GetForumAsync(int forumId);
    Task<List<ForumDto>> GetForumsAsync();
    Task<ForumWithPostDto> GetForumWithPostAsync(int forumId);
}