using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Domain.Entities.ForumEntities;
using Microsoft.EntityFrameworkCore;

namespace FMSEvaluering.Infrastructure.Queries;

public class ForumQuery : IForumQuery
{
    private readonly EvaluationContext _db;

    public ForumQuery(EvaluationContext db)
    {
        _db = db;
    }

    async Task<ForumDto> IForumQuery.GetForumAsync(int forumId)
    {
        var forum = await _db.Forums.FindAsync(forumId);

        return MapToDto(forum);
    }

    async Task<ForumWithPostDto> IForumQuery.GetForumWithPostAsync(int forumId)
    {
        var forumWithPosts = await _db.Forums
            .AsNoTracking()
            .Where(f => f.Id == forumId)
            .Include(f => f.Posts)
            .Select(f => new ForumWithPostDto
            {
                Id = f.Id,
                Name = f.Name,
                ForumType = f.GetType().Name,
                ClassId = 2, // test
                Posts = f.Posts.Select(p => new PostDto
                {
                    Id = p.Id,
                    Description = p.Description,
                    Solution = p.Solution,
                    AppUserId = p.AppUserId,
                    CreatedDate = p.CreatedDate.ToShortDateString(),
                    UpVotes = p.Votes.Count(v => v.VoteType == true),
                    DownVotes = p.Votes.Count(v => v.VoteType == false),
                }).ToList()
            })
            .SingleAsync();

        return forumWithPosts;
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
            .Select(f => new ForumWithPostDto
            {
                Id = f.Id,
                Name = f.Name,
                ForumType = f.GetType().Name,
                Posts = f.Posts.Where(p => p.Votes.Count(v => v.VoteType == true) >= reqUpvotes)
                    .Select(p => new PostDto
                    {
                        Id = p.Id,
                        Description = p.Description,
                        Solution = p.Solution,
                        AppUserId = p.AppUserId,
                        CreatedDate = p.CreatedDate.ToShortDateString(),
                        UpVotes = p.Votes.Count(v => v.VoteType == true),
                        DownVotes = p.Votes.Count(v => v.VoteType == false),
                    }).ToList()
            })
            .SingleAsync();
        return forum;
    }
}