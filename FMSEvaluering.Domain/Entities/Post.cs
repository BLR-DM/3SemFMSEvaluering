using FMSEvaluering.Domain.Values;

namespace FMSEvaluering.Domain.Entities;

public class Post : DomainEntity
{
    private readonly List<Comment> _comments = [];
    private readonly List<Vote> _votes = [];
    private readonly List<PostHistory> _history = [];

    protected Post()
    {
    }

    private Post(string description, string solution)
    {
        Description = description;
        Solution = solution;
    }

    public string Description { get; protected set; }
    public string Solution { get; protected set; }
    public IReadOnlyCollection<PostHistory> History => _history;
    public IReadOnlyCollection<Vote> Votes => _votes;
    public IReadOnlyCollection<Comment> Comments => _comments;

    public static Post Create(string description, string solution)
    {
        return new Post(description, solution);
    }

    public void SetPostHistory(PostHistory postHistory)
    {
        _history.Add(postHistory);
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

    // Comment

    public void CreateComment(string text)
    {
        var comment = Comment.Create(text);
        _comments.Add(comment);
    }

    public Comment UpdateComment(int commentId, string text)
    {
        var comment = Comments.FirstOrDefault(c => c.Id == commentId);
        if (comment is null)
            throw new ArgumentException("Denne kommentar findes ikke");

        comment.Update(text);
        return comment;
    }
}