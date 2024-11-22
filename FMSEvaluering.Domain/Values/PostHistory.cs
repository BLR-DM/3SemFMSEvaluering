namespace FMSEvaluering.Domain.Values;
public record PostHistory
{
    protected PostHistory() {}

    public PostHistory(string content)
    {
        Content = content;
        EditedDate = DateTime.Now;
    }
    public string Content { get; private set; }
    public DateTime EditedDate{ get; private set; }
}