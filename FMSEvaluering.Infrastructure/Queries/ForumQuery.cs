using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Application.Queries.QueryDto;
using FMSEvaluering.Infrastructure.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FMSEvaluering.Infrastructure.Queries;

public class ForumQuery : IForumQuery
{
    private readonly EvaluationContext _db;
    private readonly IForumMapper _forumMapper;
    private readonly IForumAccessHandler _forumAccessHandler;

    public ForumQuery(EvaluationContext db, IForumMapper forumMapper, IForumAccessHandler forumAccessHandler)
    {
        _db = db;
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

        // Validate Acccess
        await _forumAccessHandler.ValidateAccessSingleForumAsync(appUserId, role, forum);


        var forumDto = _forumMapper.MapToDtoWithAll(forum);

        return forumDto;
    }

    async Task<IEnumerable<ForumDto>> IForumQuery.GetForumsAsync(string appUserId, string role)
    {
        var forums = await _db.Forums.AsNoTracking().ToListAsync();

        var validatedForums = await _forumAccessHandler.ValidateAccessMultipleForumsAsync(appUserId, role, forums);

        return validatedForums.Select(forum => _forumMapper.MapToDto(forum)).ToList();
    }

    async Task<ForumDto> IForumQuery.GetForumWithPostsForTeacherAsync(int forumId, string appUserId, string role, int reqUpvotes)
    {
        var forum = await _db.Forums.AsNoTracking()
            .Where(f => f.Id == forumId)
            .Include(f => f.Posts)
            .ThenInclude(p => p.Votes)
            .Include(f => f.Posts)
            .ThenInclude(p => p.Comments)
            .Include(f => f.Posts)
            .ThenInclude(p => p.History)
            .SingleOrDefaultAsync();

        if (forum == null)
            throw new ArgumentException("Forum not found");

        // Validate Acccess
        await _forumAccessHandler.ValidateAccessSingleForumAsync(appUserId, role, forum);


        var forumDto = _forumMapper.MapToDtoWithAllTeacher(forum, reqUpvotes);

        return forumDto;
    }

    async Task<ForumDto> IForumQuery.GetForumWithSinglePostAsync(int forumId, string appUserId, string role, int postId)
    {
        var forum = await _db.Forums.AsNoTracking()
            .Where(f => f.Id == forumId)
            .Include(f => f.Posts.Where(p => p.Id == postId))
            .ThenInclude(p => p.Votes)
            .Include(f => f.Posts.Where(p => p.Id == postId))
            .ThenInclude(p => p.Comments)
            .Include(f => f.Posts.Where(p => p.Id == postId))
            .ThenInclude(p => p.History)
            .SingleOrDefaultAsync();

        if (forum == null)
            throw new ArgumentException("Forum not found");

        // Validate Access
        await _forumAccessHandler.ValidateAccessSingleForumAsync(appUserId, role, forum);

        var forumDto = _forumMapper.MapToDtoWithAll(forum);
        return forumDto;

    }
}