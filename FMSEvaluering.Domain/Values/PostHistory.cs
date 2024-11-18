namespace FMSEvaluering.Domain.Values;
public record PostHistory
{
    protected PostHistory() {}

    public PostHistory(string content)
    {
        Content = content;
    }
    public string Content { get; private set; }
}