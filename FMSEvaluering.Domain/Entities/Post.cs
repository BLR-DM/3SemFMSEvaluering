namespace FMSEvaluering.Domain.Entities;

public class Post : DomainEntity
{
    private readonly List<Vote> _votes = [];

    public string Description { get; protected set; }
    public string Solution { get; protected set; }
    public IReadOnlyCollection<Vote> Votes => _votes;

    protected Post() {}

    private Post(string description, string solution)
    {
        Description = description;
        Solution = solution;
    }

    public static Post Create(string description, string solution)
    {
        return new Post(description, solution);
    }

    // Vote

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
        var vote = _votes.FirstOrDefault(v => v.Id == voteId);
        _votes.Remove(vote);
    }
}