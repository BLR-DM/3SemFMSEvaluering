using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;

namespace FMSEvaluering.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly EvaluationContext _db;

    public PostRepository(EvaluationContext db)
    {
        _db = db;
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

    void IPostRepository.UpdateComment(Comment comment, byte[] rowVersion)
    {
        _db.Entry(comment).Property(nameof(comment.RowVersion)).OriginalValue = rowVersion;
    }
}