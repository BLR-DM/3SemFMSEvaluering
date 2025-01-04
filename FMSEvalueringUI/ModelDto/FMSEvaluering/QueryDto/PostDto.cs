namespace FMSEvalueringUI.ModelDto.FMSEvaluering.QueryDto;

public record PostDto()
{
    public string Id { get; set; }
    public string Description { get; set; }
    public string Solution { get; set; }
    //public string AppUserId { get; set; } // Kommenteret ud pga annonymitet
    public string CreatedDate { get; set; }
    public int UpVotes { get; set; }
    public int DownVotes { get; set; }
    public byte[] RowVersion { get; set; }
    public IEnumerable<PostHistoryDto> History { get; set; }
    public IEnumerable<VoteDto> Votes { get; set; }
    public IEnumerable<CommentDto> Comments { get; set; }

    // IEnumerable Votes
    // IEnumerable Comments
    // StudentId
}