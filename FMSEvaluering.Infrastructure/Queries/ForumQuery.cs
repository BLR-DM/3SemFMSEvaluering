using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Domain.DomainServices;
using FMSEvaluering.Domain.Entities.ForumEntities;
using FMSEvaluering.Infrastructure.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Infrastructure.Queries;

public class ForumQuery : IForumQuery
{
    private readonly EvaluationContext _db;
    private readonly IServiceProvider _serviceProvider;
    private readonly IForumMapper _forumMapper;
    private readonly IForumAccessHandler _forumAccessHandler;

    public ForumQuery(EvaluationContext db, IServiceProvider serviceProvider, IForumMapper forumMapper, IForumAccessHandler forumAccessHandler)
    {
        _db = db;
        _serviceProvider = serviceProvider;
        _forumMapper = forumMapper;
        _forumAccessHandler = forumAccessHandler;
    }

    async Task<ForumDto> IForumQuery.GetForumAsync(int forumId)
    {
        var forum = await _db.Forums.FindAsync(forumId);

        return _forumMapper.MapToDto(forum);
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

        var hasAccess = await _forumAccessHandler.ValidateAccessSingleForumAsync(appUserId, role, forum);

        if (!hasAccess)
            throw new UnauthorizedAccessException("You do not have access");

        var forumDto = _forumMapper.MapToDtoWithAll(forum);

        return forumDto;
    }

    async Task<IEnumerable<ForumDto>> IForumQuery.GetForumsAsync(string appUserId, string role)
    {
        var forums = await _db.Forums.AsNoTracking().ToListAsync();

        IEnumerable<Forum> validatedForums = await _forumAccessHandler.ValidateAccessMultipleForumsAsync(appUserId, role, forums);

        List<ForumDto> validatedForumDtos = new List<ForumDto>();
        foreach (var forum in validatedForums)
        {
            validatedForumDtos.Add(_forumMapper.MapToDto(forum));
        }
        
        return validatedForumDtos;
    }

    async Task<ForumDto> IForumQuery.GetForumWithPostsForTeacherAsync(int forumId, string role, int reqUpvotes)
    {
        var forum = await _db.Forums.AsNoTracking()
            .Where(f => f.Id == id)
            .Include(f => f.Posts)
            .ThenInclude(p => p.Votes)
            .Include(f => f.Posts)
            .ThenInclude(p => p.Comments)
            .SingleOrDefaultAsync();

        if (forum == null)
            throw new ArgumentException("Forum not found");

        var hasAccess = await _forumAccessHandler.ValidateAccessSingleForumAsync(appUserId, role, forum);

        if (!hasAccess)
            throw new UnauthorizedAccessException("You do not have access");

        var forumDto = _forumMapper.MapToDtoWithAllTeacher(forum, reqUpvotes);

        return forumDto;
    }
}