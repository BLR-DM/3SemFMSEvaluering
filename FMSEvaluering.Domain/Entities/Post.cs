namespace FMSEvaluering.Domain.Entities;

public class Post : DomainEntity
{
    private readonly List<Comment> _comments = new List<Comment>();
    public IReadOnlyCollection<Comment> Comments { get; protected set; }
    protected Post() {}

    private Post(string description)
    {
        Description = description;
    }

    public string Description { get; protected set; }

    public static Post Create(string description)
    {
        return new Post(description);
    }

    public void CreateComment(string text)
    {
        var comment = Comment.Create(text);
        _comments.Add(comment);
    }
    public Comment UpdateComment(int commentID, string text)
    {
        var comment = Comments.FirstOrDefault(c => c.Id == commentID);
        if (comment is null)
            throw new ArgumentException("Denne kommentar findes ikke");

        comment.Update(text);
        return comment;
    }
}