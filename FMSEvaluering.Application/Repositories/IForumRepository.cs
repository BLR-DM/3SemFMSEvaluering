using FMSEvaluering.Domain.Entities.ForumEntities;
using FMSEvaluering.Domain.Entities.PostEntities;

namespace FMSEvaluering.Application.Repositories;

public interface IForumRepository
{
    Task AddForum(Forum forum);
    Task<Forum> GetForumAsync(int id);
    void DeleteForum(Forum forum);
    void UpdatePost(Post post, byte[] rowVersion);
    void DeletePost(Post post, byte[] rowVersion);
}