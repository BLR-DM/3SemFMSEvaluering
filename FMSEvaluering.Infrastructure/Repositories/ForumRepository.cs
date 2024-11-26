using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities.ForumEntities;
using Microsoft.EntityFrameworkCore;

namespace FMSEvaluering.Infrastructure.Repositories;

public class ForumRepository : IForumRepository
{
    private readonly EvaluationContext _db;

    public ForumRepository(EvaluationContext db)
    {
        _db = db;
    }

    async Task IForumRepository.AddForum(Forum forum)
    {
        await _db.Forums.AddAsync(forum);
    }

    void IForumRepository.DeleteForum(Forum forum)
    {
        _db.Forums.Remove(forum);
    }

    async Task<Forum> IForumRepository.GetForum(int id)
    {
        return await _db.Forums.SingleAsync(f => f.Id == id);
    }
}