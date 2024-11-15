namespace FMSEvaluering.Domain.Entities;

public class Post : DomainEntity
{
    private readonly List<Vote> _votes = new List<Vote>();

    protected Post() {}

    private Post(string description)
    {
        Description = description;
    }

    public string Description { get; protected set; }

    public IReadOnlyCollection<Vote> Votes { get; protected set; }

    public static Post Create(string description)
    {
        return new Post(description);
    }

    public void CreateVote(bool voteType)
    {
        var vote = Vote.Create(voteType);
        _votes.Add(vote);
    }

    public Vote UpdateVote(int voteId, bool voteType)
    {
        var vote = _votes.FirstOrDefault(v => v.Id == voteId);
        vote.Update(voteType);

        return vote;
    }

    public void DeleteVote(int voteId)
    {
        _votes.Remove(_votes.FirstOrDefault(v => v.Id == voteId));
    }
}