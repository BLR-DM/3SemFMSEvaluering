using FMSEvaluering.Domain.Entities.ForumEntities;
using FMSEvaluering.Domain.Entities.PostEntities;
using Microsoft.VisualBasic;

namespace FMSEvaluering.Application.Repositories;

public interface IForumRepository
{
    Task AddForum(Forum forum);
    Task<Forum> GetForumAsync(int id);
    void DeleteForum(Forum forum, byte[] rowVersion);
    void UpdatePost(Post post, byte[] rowVersion);
    void DeletePost(Post post, byte[] rowVersion);
}