namespace FMSEvaluering.Domain.Entities;

public class Post : DomainEntity
{
    public string Description { get; protected set; }
    public string Solution { get; protected set; }

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
}