using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    async Task<Post> IPostRepository.GetPost(int id)
    {
        return await _db.Posts
            .Include(p => p.Votes)
            .Include(p => p.Comments)
            .SingleAsync(p => p.Id == id);
    }

    async Task IPostRepository.DeletePost(Post post)
    {
        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();
    }

    async Task IPostRepository.AddVote()
    {
       await _db.SaveChangesAsync();
    }

    async Task IPostRepository.DeleteVote()
    {
        await _db.SaveChangesAsync();
    }

    async Task IPostRepository.UpdateVote()
    {
        await _db.SaveChangesAsync();
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