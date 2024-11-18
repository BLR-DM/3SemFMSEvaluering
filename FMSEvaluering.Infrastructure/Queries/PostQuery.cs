using FMSEvaluering.Application.Commands.CommandDto;
using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Application.Queries.QueryDto;
using Microsoft.EntityFrameworkCore;
using PostHistoryDto = FMSEvaluering.Application.Queries.QueryDto.PostHistoryDto;

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
        var post = await _db.Posts.AsNoTracking()
            .Include(p => p.History)
            .Include(p => p.Comments)
            .Include(p => p.Votes)
            .SingleAsync(p => p.Id == postId);

        return new PostDto
        {
            Id = post.Id,
            Description = post.Description,
            Solution = post.Solution,
            PostHistoryDto = post.History.Select(ph => new PostHistoryDto
            {
                Content = ph.Content
            }),
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