namespace FMSEvaluering.Domain.Entities.PostEntities;

public class Vote : DomainEntity
{
    public string AppUserId { get; set; }
    public bool VoteType { get; protected set; }

    protected Vote() { }

    private Vote(bool voteType, string appUserId)
    {
        AppUserId = appUserId;
        VoteType = voteType;
    }

    public static Vote Create(bool voteType, string appUserId)
    {
        return new Vote(voteType, appUserId);
    }

    public void Update(bool voteType)
    {
        VoteType = voteType;
    }
}