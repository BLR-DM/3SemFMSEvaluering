using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Domain.Entities.ForumEntities;

namespace FMSEvaluering.Infrastructure.Helpers.Interfaces;

public interface IForumMapper
{
    ForumDto MapToDto(Forum forum);

    ForumDto MapToDtoWithAll(Forum forum);
    ForumDto MapToDtoWithAllTeacher(Forum forum, int reqUpvotes);
}