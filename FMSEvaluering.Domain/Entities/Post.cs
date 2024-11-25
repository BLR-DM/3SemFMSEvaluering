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

    private Post(string description, string solution, string appUserId)
    {
        Description = description;
        Solution = solution;
        AppUserId = appUserId;
    }

    public string Description { get; protected set; }
    public string Solution { get; protected set; }
    public string AppUserId { get; protected set; }
    // public int ForumId { get; protected set; } // Mangler implementering
    public ICollection<PostHistory> History => _history;
    public IReadOnlyCollection<Vote> Votes => _votes;
    public IReadOnlyCollection<Comment> Comments => _comments;

    public static Post Create(string description, string solution, string appUserId)
    {
        return new Post(description, solution, appUserId);
    }

    public void SetPostHistory(PostHistory postHistory)
    {
        _history.Add(postHistory);
    }

    // Vote

    public void HandleVote(bool voteType, string appUserId)
    {
        var vote = Votes.FirstOrDefault(v => v.AppUserId == appUserId);

        if (vote == null)
        {
            CreateVote(voteType, appUserId);
        }
        else if (vote.VoteType == voteType)
        {
            DeleteVote(appUserId);
        }
        else
        {
            UpdateVote(voteType, appUserId);
        }
    }

    public void CreateVote(bool voteType, string appUserId)
    {
        if (Votes.Any(v => v.AppUserId == appUserId))
        {
            throw new InvalidOperationException("User has already voted");
        }

        var vote = Vote.Create(voteType, appUserId);
        _votes.Add(vote);
    }

    public Vote UpdateVote(bool voteType, string appUserId)
    {
        var vote = _votes.FirstOrDefault(v => v.AppUserId == appUserId);

        vote.Update(voteType);

        return vote;
    }

    public void DeleteVote(string appUserId)
    {
        var vote = _votes.FirstOrDefault(v => v.AppUserId == appUserId);
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