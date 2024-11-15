namespace FMSEvaluering.Domain.Entities;

public class Post : DomainEntity
{
    protected Post()
    {
    }

    private Post(string description)
    {
        Description = description;
    }

    public string Description { get; protected set; }

    public static Post Create(string description)
    {
        return new Post(description);
    }
}