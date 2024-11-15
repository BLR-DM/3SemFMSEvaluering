using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities;

namespace FMSEvaluering.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly EvaluationContext _db;

        public PostRepository(EvaluationContext db)
        {
            _db = db;
        }

        async Task IPostRepository.AddEvaluationPost(Post post)
        {
            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();
        }
    }
}
