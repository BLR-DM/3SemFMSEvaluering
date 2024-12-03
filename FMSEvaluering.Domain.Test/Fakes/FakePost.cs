using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Domain.Entities.PostEntities;

namespace FMSEvaluering.Domain.Test.Fakes
{
    public class FakePost : Post
    {
        public FakePost(string appUserId) : base()
        {
            AppUserId = appUserId;
        }

        public new void CreateVote(bool voteType, string appUserId)
        {
            base.CreateVote(voteType, appUserId);
        }

        public new HandleVoteBehaviour HandleVote(bool voteType, string appUserId)
        {
            return base.HandleVote(voteType, appUserId);
        }

        public void AddVote(Vote vote)
        {
            _votes.Add(vote);
        }
    }
}
