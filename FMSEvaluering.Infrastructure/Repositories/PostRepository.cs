using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace FMSEvaluering.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly EvaluationContext _db;

    public PostRepository(EvaluationContext db)
    {
        _db = db;
    }

    async Task IPostRepository.AddPost(Post post)
    {
        try
        {
            await _db.Posts.AddAsync(post);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    async Task<Post> IPostRepository.GetPostAsync(int id)
    {
        try
        {
            return await _db.Posts
                .Include(p => p.Votes)
                .Include(p => p.Comments)
                .Include(p => p.History)
                .SingleAsync(p => p.Id == id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    void IPostRepository.UpdatePost(Post post, byte[] rowVersion)
    {
        _db.Entry(post).Property(nameof(post.RowVersion)).OriginalValue = rowVersion;
    }

    void IPostRepository.DeletePost(Post post, byte[] rowVersion)
    {
        _db.Entry(post).Property(nameof(post.RowVersion)).OriginalValue = rowVersion;
        _db.Posts.Remove(post);
    }

    void IPostRepository.UpdateCommentAsync(Comment comment, byte[] rowVersion)
    {
        _db.Entry(comment).Property(nameof(comment.RowVersion)).OriginalValue = rowVersion;
    }
}