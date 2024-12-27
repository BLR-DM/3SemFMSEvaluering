using FMSEvaluering.Domain.Entities.ForumEntities;
using FMSEvaluering.Domain.Entities.PostEntities;
using Microsoft.VisualBasic;

namespace FMSEvaluering.Application.Repositories;

public interface IForumRepository
{
    Task AddForumAsync(Forum forum);
    Task<Forum> GetForumAsync(int id);
    Task<Forum> GetForumWithSinglePostAsync(int forumId, int postId);
    void DeleteForum(Forum forum, byte[] rowVersion);
    void UpdatePost(Post post, byte[] rowVersion);
    void DeletePost(Post post, byte[] rowVersion);
    void UpdateComment(Comment comment, byte[] rowVersion);
    void UpdateVote(Vote vote, byte[] rowVersion);
    void DeleteVote(Vote vote, byte[] rowVersion);
}