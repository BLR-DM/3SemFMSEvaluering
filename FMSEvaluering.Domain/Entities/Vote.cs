namespace FMSEvaluering.Domain.Entities;

public class Vote : DomainEntity
{
    public string AppUserId { get; set; }
    public bool VoteType { get; protected set; }

    protected Vote() { }

    private Vote(bool voteType, string appUserId, IEnumerable<Vote> existingVotes)
    {
        AppUserId = appUserId;
        VoteType = voteType;
    }

    public static Vote Create(bool voteType, string appUserId, IEnumerable<Vote> existingVotes)
    {
        return new Vote(voteType, appUserId, existingVotes);
    }

    public void Update(bool voteType)
    {
        VoteType = voteType;
    }



}