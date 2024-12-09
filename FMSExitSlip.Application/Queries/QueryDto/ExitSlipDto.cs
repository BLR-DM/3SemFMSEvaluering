namespace FMSExitSlip.Application.Queries.QueryDto;

public record ExitSlipDto
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; }
    public string Title { get; set; }
    public int MaxQuestions { get; set; }
    public bool IsPublished { get; set; }
    public string AppUserId { get; set; }
    public int LectureId { get; set; }
    public IEnumerable<QuestionDto> Questions { get; set; }

    public int StudentCount { get; set; }
    public int ParticipationCount { get; set; }
}