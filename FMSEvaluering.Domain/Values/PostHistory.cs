namespace FMSEvaluering.Domain.Values;
public record PostHistory
{
    protected PostHistory() {}

    public PostHistory(string description, string solution)
    {
        Description = description;
        Solution = solution;
        EditedDate = DateTime.Now;
    }
    public string Description { get; private set; }
    public string Solution { get; private set; }
    public DateTime EditedDate { get; private set; }
}