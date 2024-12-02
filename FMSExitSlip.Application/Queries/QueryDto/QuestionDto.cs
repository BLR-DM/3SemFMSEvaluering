namespace FMSExitSlip.Application.Queries.QueryDto;

public record QuestionDto
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; }
    public string Text { get; set; }
    public string AppUserId { get; set; }
    public IEnumerable<ResponseDto> Responses { get; set; }
}