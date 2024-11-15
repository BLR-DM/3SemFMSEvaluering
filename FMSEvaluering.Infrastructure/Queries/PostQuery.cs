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
    async Task<PostDto> IPostQuery.GetPostAsync(int postId)
    {
        //var post = await _db.Posts.AsNoTracking()
        //    .SingleAsync(p => p.Id == postId);

        //return new PostDto
        //{
        //    Id = post.Id,
        //    Description = post.Description,
        //    Solution = post.Solution
        //};

        var post = await _db.Posts.AsNoTracking().Include(p => p.Comments).Include(p => p.Votes)
            .SingleAsync(p => p.Id == postId);

        return new PostDto
        {
            Id = post.Id,
            Description = post.Description,
            Solution = post.Solution,
            VoteDto = post.Votes.Select(v => new VoteDto
            {
                Id = v.Id,
                VoteType = v.VoteType
            }),
            CommentDto = post.Comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Text = c.Text
            })
        };
    }
}