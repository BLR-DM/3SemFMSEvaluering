using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities;

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
        await _db.Posts.AddAsync(post);
        await _db.SaveChangesAsync();
    }

    Task<Post> IPostRepository.GetPost(int id)
    {
        throw new NotImplementedException();
    }

    async Task IPostRepository.AddCommentAsync()
    {
        await _db.SaveChangesAsync();
    }

    async Task IPostRepository.UpdateCommentAsync(Comment comment, byte[] rowVersion)
    {
        _db.Entry(comment).Property(nameof(comment.RowVersion)).OriginalValue = rowVersion;

        await _db.SaveChangesAsync();
    }
}