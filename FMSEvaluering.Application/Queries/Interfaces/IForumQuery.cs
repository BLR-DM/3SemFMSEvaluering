using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Domain.Entities.ForumEntities;

namespace FMSEvaluering.Application.Queries.Interfaces;

public interface IForumQuery
{
    Task<ForumDto> GetForumAsync(int forumId);
    Task<IEnumerable<ForumDto>> GetForumsAsync(string appUserId, string role);
    Task<ForumDto> GetForumWithPostsAsync(int forumId, string appUserId, string role);
    Task<ForumDto> GetForumWithPostsForTeacherAsync(int forumId, string appUserId, string role, int reqUpvotes);
    Task<ForumDto> GetForumWithSinglePostAsync(int forumId, string appUserId, string role, int postId);
}