using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Domain.Entities.ForumEntities;

namespace FMSEvaluering.Application.Repositories
{
    public interface IForumRepository
    {
        Task AddForum(Forum forum);
        Task<Forum> GetForumAsync(int id);
        void DeleteForum(Forum forum);
    }
}
