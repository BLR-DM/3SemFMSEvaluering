using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Domain.Entities.ForumEntities;

namespace FMSEvaluering.Application.Queries.Interfaces;

public interface IForumQuery
{
    Task<ForumDto> GetForumAsync(int forumId);
    Task<List<ForumDto>> GetForumsAsync();
    Task<ForumDto> GetForumWithPostsAsync(int forumId, string appUserId, string role);
    Task<ForumWithPostDto> GetForumWithPostsForTeacherAsync(int id, int reqUpvotes);
}