using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Application.Queries.QueryDto;
using Microsoft.EntityFrameworkCore;

namespace FMSEvaluering.Infrastructure.Queries;

public class PostQuery : IPostQuery
{
    private readonly EvaluationContext _db;

    public PostQuery(EvaluationContext db)
    {
        _db = db;
    }
    async Task<PostDto> IPostQuery.GetPost(int postId)
    {
        var post = await _db.Posts.AsNoTracking()
            .SingleAsync(p => p.Id == postId);

        return new PostDto
        {
            Id = post.Id,
            Description = post.Description,
            Solution = post.Solution
        };
    }
}