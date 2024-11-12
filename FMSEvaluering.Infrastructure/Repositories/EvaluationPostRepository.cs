using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities;

namespace FMSEvaluering.Infrastructure.Repositories
{
    public class EvaluationPostRepository : IEvaluationPostRepository
    {
        private readonly EvaluationContext _db;

        public EvaluationPostRepository(EvaluationContext db)
        {
            _db = db;
        }

        async Task IEvaluationPostRepository.AddEvaluationPost(EvaluationPost evaluationPost)
        {
            await _db.EvaluationPosts.AddAsync(evaluationPost);
            await _db.SaveChangesAsync();
        }
    }
}
