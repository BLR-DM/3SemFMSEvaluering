using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Domain.Entities.Forum;
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
}