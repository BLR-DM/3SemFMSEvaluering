using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Domain.Entities.ForumEntities;

namespace FMSEvaluering.Infrastructure.Helpers.Interfaces;

public interface IForumAccessHandler
{
    Task<bool> ValidateAccessSingleForumAsync(string appUserId, string role, Forum forum);
    Task<IEnumerable<Forum>> ValidateAccessMultipleForumsAsync(string appUserId, string role, List<Forum> forums);
}