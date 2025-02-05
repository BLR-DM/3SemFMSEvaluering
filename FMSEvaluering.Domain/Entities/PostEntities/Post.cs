﻿using FMSEvaluering.Domain.Values;

namespace FMSEvaluering.Domain.Entities.PostEntities;

public class Post : DomainEntity
{
    private readonly List<Comment> _comments = [];
    protected readonly List<Vote> _votes = [];
    private readonly List<PostHistory> _history = [];

    protected Post()
    {
    }

    private Post(string description, string solution, string appUserId)
    {
        Description = description;
        Solution = solution;
        AppUserId = appUserId;
        CreatedDate = DateTime.Now;
    }

    public string Description { get; protected set; }
    public string Solution { get; protected set; }
    public string AppUserId { get; protected set; }
    public DateTime CreatedDate { get; private set; }
    public ICollection<PostHistory> History => _history;
    public IReadOnlyCollection<Vote> Votes => _votes;
    public IReadOnlyCollection<Comment> Comments => _comments;

    public static Post Create(string description, string solution, string appUserId)
    {
        return new Post(description, solution, appUserId);
    }

    public void Update(string newDescription, string newSolution,  string userId)
    {
        AssureUserIsCreator(userId);

        SetHistory(Description, Solution);
        Description = newDescription;
        Solution = newSolution;
    }

    private void SetHistory(string orgDescription, string orgSolution)
    {
        _history.Add(new PostHistory(orgDescription, orgSolution));
    }

    private void AssureUserIsCreator(string userId)
    {
        if (!AppUserId.Equals(userId))
            throw new ArgumentException("Only the creater of the post can edit it");
    }

    // Vote

    public HandleVoteBehaviour HandleVote(bool voteType, string appUserId)
    {
        var vote = Votes.FirstOrDefault(v => v.AppUserId == appUserId);

        if (vote == null)
        {
            CreateVote(voteType, appUserId);
            return HandleVoteBehaviour.Create;
        }
        else if (vote.VoteType == voteType)
        {
            return HandleVoteBehaviour.Delete;
        }
        else
        {
            UpdateVote(voteType, appUserId);
            return HandleVoteBehaviour.Update;
        }
    }

    public enum HandleVoteBehaviour
    {
        Create,
        Update,
        Delete
    }

    public void CreateVote(bool voteType, string appUserId)
    {
        if (Votes.Any(v => v.AppUserId == appUserId))
            throw new InvalidOperationException("User has already voted");

        var vote = Vote.Create(voteType, appUserId);
        _votes.Add(vote);
    }

    public Vote UpdateVote(bool voteType, string appUserId)
    {
        var vote = _votes.FirstOrDefault(v => v.AppUserId == appUserId);
        if (vote is null) throw new ArgumentException("Vote not found");

        vote.Update(voteType);
        return vote;
    }

    public void DeleteVote(string appUserId)
    {
        var vote = _votes.FirstOrDefault(v => v.AppUserId == appUserId);
        if (vote is null) throw new ArgumentException("Vote not found");
        _votes.Remove(vote);
    }

    // Comment

    public void CreateComment(string firstName, string lastName, string text, string appUserId)
    {
        var comment = Comment.Create(firstName, lastName, text, appUserId);
        _comments.Add(comment);
    }

    public Comment UpdateComment(int commentId, string text, string appUserId)
    {
        if (!AppUserId.Equals(appUserId))
            throw new ArgumentException("Only the creater of the post can edit it");

        var comment = Comments.FirstOrDefault(c => c.Id == commentId);
        if (comment is null) throw new ArgumentException("Comment not found");

        comment.Update(text);
        return comment;
    }
}