namespace FMSEvaluering.Application.Queries.QueryDto;

public record PostDto()
{
    public string Description { get; set; }
    public string Solution { get; set; }
    public string AppUserId { get; set; }
    public string CreatedDate { get; set; }
    public int UpVotes { get; set; }
    public int DownVotes { get; set; }
    public IEnumerable<PostHistoryDto> PostHistoryDto { get; set; }
    //public IEnumerable<VoteDto> VoteDto { get; set; }
    public IEnumerable<CommentDto> CommentDto { get; set; }

    // IEnumerable VoteDto
    // IEnumerable CommentDto
    // StudentId
}