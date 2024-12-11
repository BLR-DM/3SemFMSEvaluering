using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Domain.Entities.ForumEntities;
using FMSEvaluering.Infrastructure.Helpers.Interfaces;

namespace FMSEvaluering.Infrastructure.Helpers;

public class ForumMapper : IForumMapper
{
    ForumDto IForumMapper.MapToDto(Forum forum)
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

    ForumDto IForumMapper.MapToDtoWithAll(Forum forum)
    {
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
                }).ToList(),
                Votes = p.Votes.Select(v => new VoteDto
                {
                    VoteType = v.VoteType,
                    RowVersion = v.RowVersion
                }).ToList(),
                Comments = p.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Text = c.Text,
                    RowVersion = c.RowVersion
                }).ToList()
            }).ToList()
        };

        return forumDto;
    }

    ForumDto IForumMapper.MapToDtoWithAllTeacher(Forum forum, int reqUpvotes)
    {
        var forumDto = new ForumDto
        {
            Id = forum.Id,
            Name = forum.Name,
            ForumType = forum.GetType().Name,
            Posts = forum.Posts.Where(p => p.Votes.Count(v => v.VoteType) >= reqUpvotes)
                .Select(p => new PostDto
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
                }).ToList(),
                Votes = p.Votes.Select(v => new VoteDto
                {
                    VoteType = v.VoteType,
                    RowVersion = v.RowVersion
                }).ToList(),
                Comments = p.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Text = c.Text,
                    RowVersion = c.RowVersion
                }).ToList()
            }).ToList()
        };

        return forumDto;
    }
}