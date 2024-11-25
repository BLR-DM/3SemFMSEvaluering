using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Domain.Entities.Forum;

namespace FMSEvaluering.Application.Repositories
{
    public interface IForumRepository
    {
        Task AddForum(Forum forum);
        Task<Forum> GetForum(int id);
        void DeleteForum(Forum forum);
    }
}
