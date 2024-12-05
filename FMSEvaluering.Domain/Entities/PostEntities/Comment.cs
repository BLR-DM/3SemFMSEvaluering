namespace FMSEvaluering.Domain.Entities.PostEntities;

public class Comment : DomainEntity
{
    protected Comment()
    {
    }

    private Comment(string firstName, string lastName, string text, string appUserId)
    {
        FirstName = firstName;
        LastName = lastName;
        Text = text;
        CreatedDate = DateTime.Now;
        AppUserId = appUserId;
    }

    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public string Text { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public string AppUserId { get; protected set; }

    public static Comment Create(string firstName, string lastName, string text, string appUserId)
    {
        return new Comment(firstName, lastName, text, appUserId);
    }

    public void Update(string text)
    {
        Text = text;
    }
}