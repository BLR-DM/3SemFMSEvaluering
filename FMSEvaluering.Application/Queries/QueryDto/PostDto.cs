namespace FMSEvaluering.Application.Queries.QueryDto;

public record PostDto()
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Solution { get; set; }
    public IEnumerable<VoteDto> VoteDto { get; set; }
    public IEnumerable<CommentDto> CommentDto { get; set; }

    // IEnumerable VoteDto
    // IEnumerable CommentDto
    // StudentId
}