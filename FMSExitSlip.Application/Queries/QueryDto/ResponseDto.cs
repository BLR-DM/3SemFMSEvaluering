namespace FMSExitSlip.Application.Queries.QueryDto;

public record ResponseDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string AppUserId { get; set; }
}