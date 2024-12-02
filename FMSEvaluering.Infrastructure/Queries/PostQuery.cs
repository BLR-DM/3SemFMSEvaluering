using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;

namespace FMSEvaluering.Infrastructure.Queries;

public class PostQuery : IPostQuery
{
    private readonly EvaluationContext _db;
    private readonly IServiceProvider _serviceProvider;

    public PostQuery(EvaluationContext db, IServiceProvider serviceProvider)
    {
        _db = db;
        _serviceProvider = serviceProvider;
    }

    async Task<PostDto> IPostQuery.GetPostAsync(int postId, string appUserId, string role)
    {
        var post = await _db.Posts.AsNoTracking()
            .Include(p => p.Forum)
            .Include(p => p.History)
            .Include(p => p.Comments)
            .Include(p => p.Votes)
            .SingleAsync(p => p.Id == postId);

        if (post.Forum == null)
            throw new ArgumentException("Forum not found");

        var hasAccess = await post.Forum.ValidateUserAccessAsync(appUserId, _serviceProvider, role);

        if (!hasAccess)
            throw new UnauthorizedAccessException("You do not have access");

        return new PostDto
        {
            Id = post.Id.ToString(),
            Description = post.Description,
            Solution = post.Solution,
            CreatedDate = post.CreatedDate.ToShortDateString(),
            UpVotes = post.Votes.Count(v => v.VoteType),
            DownVotes = post.Votes.Count(v => !v.VoteType),
            History = post.History.Select(ph => new PostHistoryDto
            {
                Content = ph.Content,
                EditedDate = ph.EditedDate.ToShortDateString()
            }).ToList(),
            Votes = post.Votes.Select(v => new VoteDto
            {
                VoteType = v.VoteType,
                RowVersion = v.RowVersion
            }).ToList(),
            Comments = post.Comments.Select(c => new CommentDto
            {
                Text = c.Text,
                RowVersion = c.RowVersion
            }).ToList()
        };
    }

    async Task<IEnumerable<PostDto>> IPostQuery.GetPostsAsync(int forumId, string appUserId, string role)
    {
        var forum = await _db.Forums.AsNoTracking()
            .Include(f => f.Posts)
                .ThenInclude(p => p.History)
            .Include(f => f.Posts)
                .ThenInclude(p => p.Comments)
            .Include(f => f.Posts)
                .ThenInclude(p => p.Votes)
            .SingleOrDefaultAsync(f => f.Id == forumId);

        if (forum == null)
            throw new ArgumentException("Forum not found");

        var hasAcceess = await forum.ValidateUserAccessAsync(appUserId, _serviceProvider, role);

        if (!hasAcceess)
            throw new UnauthorizedAccessException("You do not have access");

        return forum.Posts.Select(post => new PostDto
        {
            Id = post.Id.ToString(),
            Description = post.Description,
            Solution = post.Solution,
            CreatedDate = post.CreatedDate.ToShortDateString(),
            UpVotes = post.Votes.Count(v => v.VoteType),
            DownVotes = post.Votes.Count(v => !v.VoteType),
            History = post.History.Select(ph => new PostHistoryDto
            {
                Content = ph.Content,
                EditedDate = ph.EditedDate.ToShortDateString()
            }).ToList(),
            Comments = post.Comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Text = c.Text
            }).ToList()
        });
    }
}