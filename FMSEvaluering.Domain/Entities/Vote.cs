namespace FMSEvaluering.Domain.Entities;

public class Vote : DomainEntity
{
    public bool VoteType { get; protected set; }


    // Nav
    public Post Post { get; protected set; }

    protected Vote() { }

    private Vote(bool voteType, Post post)
    {
        VoteType = voteType;
        Post = post;
    }

    public static Vote Create(bool voteType, Post post)
    {
        return new Vote(voteType, post);
    }
}