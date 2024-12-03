using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Domain.Entities.PostEntities;

namespace FMSEvaluering.Domain.Test.Fakes
{
    public class FakeVote : Vote
    {
        public FakeVote(bool voteType, string appUserId) : base()
        {
            VoteType = voteType;
            AppUserId = appUserId;
        }
    }
}
