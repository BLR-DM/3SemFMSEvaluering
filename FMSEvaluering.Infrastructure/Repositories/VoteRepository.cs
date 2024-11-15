using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities;

namespace FMSEvaluering.Infrastructure.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly EvaluationContext _db;

        public VoteRepository(EvaluationContext context)
        {
            _db = context;
        }

        void IVoteRepository.AddVote(Vote vote)
        {
            _db.Votes.Add(vote);
            _db.SaveChanges();
        }
    }
}
