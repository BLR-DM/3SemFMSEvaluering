using FMSEvaluering.Domain.Entities.ForumEntities;

namespace FMSEvaluering.Application.Helpers;

public interface IForumAccessHandler
{
    Task ValidateAccessSingleForumAsync(string appUserId, string role, Forum forum);
    Task<IEnumerable<Forum>> ValidateAccessMultipleForumsAsync(string appUserId, string role, List<Forum> forums);
}