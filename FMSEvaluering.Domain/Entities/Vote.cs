namespace FMSEvaluering.Domain.Entities;

public class Vote : DomainEntity
{
    public bool VoteType { get; protected set; }

    protected Vote() { }

    private Vote(bool voteType)
    {
        VoteType = voteType;
    }

    public static Vote Create(bool voteType)
    {
        return new Vote(voteType);
    }

    public void Update(bool voteType)
    {
        VoteType = voteType;
    }

}