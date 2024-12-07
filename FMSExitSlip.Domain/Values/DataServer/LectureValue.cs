namespace FMSExitSlip.Domain.Values.DataServer;

public record LectureValue
{
    public string Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
}