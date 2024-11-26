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
        List<FakeVote> _fakeVotes = new List<FakeVote>();
        public FakePost(string description, string solution, string appUserId, List<FakeVote> fakeVotes)
        {
            Description = description;
            Solution = solution;
            AppUserId = appUserId;
            _fakeVotes = fakeVotes;
        }

        public new void HandleVotes(bool voteType, string AppUserId)
        {
            base.HandleVote(voteType, AppUserId);
        }
    }
}
