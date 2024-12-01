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

    async Task<PostDto> IPostQuery.GetPostAsync(int postId, string appUserId)
    {
        var post = await _db.Posts.AsNoTracking()
            .Include(p => p.Forum)
            .Include(p => p.History)
            .Include(p => p.Comments)
            .Include(p => p.Votes)
            .SingleAsync(p => p.Id == postId);

        if (post.Forum == null)
            throw new ArgumentException("Forum not found");

        var hasAccess = await post.Forum.ValideUserAccessToForum(appUserId);

        if (!hasAccess)
            throw new UnauthorizedAccessException("You do not have access");

        return new PostDto
        {
            Description = post.Description,
            Solution = post.Solution,
            CreatedDate = post.CreatedDate.ToShortDateString(),
            UpVotes = post.Votes.Count(v => v.VoteType == true),
            DownVotes = post.Votes.Count(v => v.VoteType == false),
            PostHistoryDto = post.History.Select(ph => new PostHistoryDto
            {
                Content = ph.Content,
                EditedDate = ph.EditedDate.ToShortDateString()
            }).ToList(),
            //VoteDto = post.Votes.Select(v => new VoteDto
            //{
            //    VoteType = v.VoteType
            //}),
            CommentDto = post.Comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Text = c.Text
            }).ToList()
        };
    }
}