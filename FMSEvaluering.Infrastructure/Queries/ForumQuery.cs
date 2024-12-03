using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Domain.Entities.ForumEntities;
using Microsoft.EntityFrameworkCore;

namespace FMSEvaluering.Infrastructure.Queries;

public class ForumQuery : IForumQuery
{
    private readonly EvaluationContext _db;
    private readonly IServiceProvider _serviceProvider;

    public ForumQuery(EvaluationContext db, IServiceProvider serviceProvider)
    {
        _db = db;
        _serviceProvider = serviceProvider;
    }

    async Task<ForumDto> IForumQuery.GetForumAsync(int forumId)
    {
        var forum = await _db.Forums.FindAsync(forumId);

        return MapToDto(forum);
    }

    async Task<ForumDto> IForumQuery.GetForumWithPostsAsync(int forumId, string appUserId, string role)
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

        var forumDto = new ForumDto
        {
            Id = forum.Id,
            Name = forum.Name,
            ForumType = forum.GetType().Name,
            Posts = forum.Posts.Select(p => new PostDto
            {
                Id = p.Id.ToString(),
                Description = p.Description,
                Solution = p.Solution,
                CreatedDate = p.CreatedDate.ToShortDateString(),
                UpVotes = p.Votes.Count(v => v.VoteType),
                DownVotes = p.Votes.Count(v => !v.VoteType),
                RowVersion = p.RowVersion,
                History = p.History.Select(ph => new PostHistoryDto
                {
                    Description = ph.Description,
                    Solution = ph.Solution,
                    EditedDate = ph.EditedDate.ToShortDateString()
                }),
                Votes = p.Votes.Select(v => new VoteDto
                {
                    VoteType = v.VoteType,
                    RowVersion = v.RowVersion
                }),
                Comments = p.Comments.Select(c => new CommentDto
                {
                    Text = c.Text,
                    RowVersion = c.RowVersion
                })
            })
        };

        return forumDto;
    }

    async Task<List<ForumDto>> IForumQuery.GetForumsAsync()
    {
        var forums = await _db.Forums.ToListAsync();
        return forums.Select(MapToDto).ToList();
    }

    private ForumDto MapToDto(Forum forum)
    {
        if (forum is PublicForum publicForum)
        {
            return new PublicForumDto
            {
                Id = publicForum.Id,
                Name = publicForum.Name,
                ForumType = nameof(PublicForum)
            };
        }
        else if (forum is ClassForum classForum)
        {
            return new ClassForumDto
            {
                Id = classForum.Id,
                Name = classForum.Name,
                ForumType = nameof(ClassForum),
                ClassId = classForum.ClassId
            };
        }
        else if (forum is SubjectForum subjectForum)
        {
            return new SubjectForumDto
            {
                Id = subjectForum.Id,
                Name = subjectForum.Name,
                ForumType = nameof(SubjectForum),
                SubjectId = subjectForum.SubjectId
            };
        }

        throw new InvalidOperationException("Unknown forum type");
    }

    async Task<ForumWithPostDto> IForumQuery.GetForumWithPostsForTeacherAsync(int id, int reqUpvotes)
    {
        var forum = await _db.Forums.AsNoTracking()
            .Where(f => f.Id == id)
            .Include(f => f.Posts)
                .ThenInclude(p => p.Votes)
            .Select(f => new ForumWithPostDto
            {
                Id = f.Id,
                Name = f.Name,
                ForumType = f.GetType().Name,
                Posts = f.Posts.Where(p => p.Votes.Count(v => v.VoteType) >= reqUpvotes)
                    .Select(p => new PostDto
                    {
                        Id = p.Id.ToString(),
                        Description = p.Description,
                        Solution = p.Solution,
                        CreatedDate = p.CreatedDate.ToShortDateString(),
                        UpVotes = p.Votes.Count(v => v.VoteType),
                        DownVotes = p.Votes.Count(v => !v.VoteType),
                    })
            })
            .SingleAsync();

        return forum;
    }
}