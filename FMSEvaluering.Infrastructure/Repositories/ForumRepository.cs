using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities.ForumEntities;
using FMSEvaluering.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;

namespace FMSEvaluering.Infrastructure.Repositories;

public class ForumRepository : IForumRepository
{
    private readonly EvaluationContext _db;

    public ForumRepository(EvaluationContext db)
    {
        _db = db;
    }

    async Task IForumRepository.AddForum(Forum forum)
    {
        await _db.Forums.AddAsync(forum);
    }

    void IForumRepository.DeleteForum(Forum forum, byte[] rowVersion)
    {
        _db.Entry(forum).Property(nameof(forum.RowVersion)).OriginalValue = rowVersion;
        _db.Forums.Remove(forum);
    }

    async Task<Forum> IForumRepository.GetForumAsync(int id)
    {
        return await _db.Forums
            .Include(f => f.Posts)
                .ThenInclude(p => p.History)
            .SingleAsync(f => f.Id == id);
    }

    async Task<Forum> IForumRepository.GetForumWithSinglePostAsync(int forumId, int postId)
    {
        return await _db.Forums
            .Include(f => f.Posts.Where(p => p.Id == postId))
                .ThenInclude(p => p.History)
            .Include(f => f.Posts.Where(p => p.Id == postId))
                .ThenInclude(p => p.Comments)
            .Include(f => f.Posts.Where(p => p.Id == postId))
                .ThenInclude(p => p.Votes)
            .SingleAsync(f => f.Id == forumId);
    }

    void IForumRepository.UpdatePost(Post post, byte[] rowVersion)
    {
        _db.Entry(post).Property(nameof(post.RowVersion)).OriginalValue = rowVersion;
    }

    void IForumRepository.DeletePost(Post post, byte[] rowVersion)
    {
        _db.Entry(post).Property(nameof(post.RowVersion)).OriginalValue = rowVersion;
        _db.Posts.Remove(post);
    }

    void IForumRepository.UpdateComment(Comment comment, byte[] rowVersion)
    {
        _db.Entry(comment).Property(nameof(comment.RowVersion)).OriginalValue = rowVersion;
    }

    void IForumRepository.UpdateVote(Vote vote, byte[] rowVersion)
    {
        _db.Entry(vote).Property(nameof(vote.RowVersion)).OriginalValue = rowVersion;
    }

    void IForumRepository.DeleteVote(Vote vote, byte[] rowVersion)
    {
        _db.Entry(vote).Property(nameof(vote.RowVersion)).OriginalValue = rowVersion;
        _db.Votes.Remove(vote);
    }
}