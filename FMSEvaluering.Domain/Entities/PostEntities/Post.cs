using FMSEvaluering.Domain.Entities.ForumEntities;
using FMSEvaluering.Domain.Values;

namespace FMSEvaluering.Domain.Entities.PostEntities;

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
        CreatedDate = DateTime.Now;

        //AssureStudentIsPartOfClass(fmsValidationResponse.ClassId); //async??
        //Forum.ValidateUserAccessAsync(appUserId, _serviceProvider, role); // async?
    }

    public string Description { get; protected set; }
    public string Solution { get; protected set; }
    public string AppUserId { get; protected set; }
    public Forum Forum { get; protected set; }
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
        AssureUserIsSameUser(userId);

        SetHistory(Description, Solution);
        Description = newDescription;
        Solution = newSolution;
    }

    private void SetHistory(string orgDescription, string orgSolution)
    {
        _history.Add(new PostHistory(orgDescription, orgSolution));
    }

    private void AssureUserIsSameUser(string userId)
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
            DeleteVote(appUserId);
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

    //public void AssureStudentIsPartOfClass(string studentClassId)
    //{
    //    if (Forum is ClassForum classForum)
    //    {
    //        if (!studentClassId.Equals(classForum.ClassId.ToString()))
    //        {
    //            throw new InvalidOperationException("You is not part of this class.");
    //        }
    //    }
    //}
}