namespace FMSEvaluering.Domain.Entities.PostEntities;

public class Comment : DomainEntity
{
    protected Comment()
    {
    }

    private Comment(string text)
    {
        Text = text;
        CreatedDate = DateTime.Now;
    }

    public string Text { get; protected set; }
    public DateTime CreatedDate { get; protected set; }

    public static Comment Create(string text)
    {
        return new Comment(text);
    }

    public void Update(string text)
    {
        Text = text;
    }
}